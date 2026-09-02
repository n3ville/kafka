using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System.IO;
using System.Net;
using System.Collections.Concurrent;
using System.Xml.Linq;
namespace Infinitum.SQL.Kafka
{
    public class Kafka
    {
        // --- ВАРИАНТ 2: VaultUrl/Token/MountPoint/Path приходят параметрами
        // хранимой процедуры (см. публичные методы ниже), сама сборка их не хранит.
        // Из Vault теперь достаём не только username/password, но и bootstrap_servers —
        // в проекте больше не должно быть захардкоженных адресов Kafka-брокеров. ---
        private static string ExtractUsername(Dictionary<string, string> creds)
        {
            if (!creds.TryGetValue("username", out var username) || string.IsNullOrWhiteSpace(username))
                throw new Exception("Vault secret is missing 'username'.");
            return username;
        }

        private static string ExtractPassword(Dictionary<string, string> creds)
        {
            if (!creds.TryGetValue("password", out var password) || string.IsNullOrWhiteSpace(password))
                throw new Exception("Vault secret is missing 'password'.");
            return password;
        }

        private static string ExtractBootstrapServers(Dictionary<string, string> creds)
        {
            if (!creds.TryGetValue("bootstrap_servers", out var servers) || string.IsNullOrWhiteSpace(servers))
                throw new Exception("Vault secret is missing 'bootstrap_servers'.");
            return servers;
        }

        private static AdminClientConfig GetAdminConfig(string vaultUrl, string vaultToken, string vaultMountPoint, string vaultPath)
        {
            var creds = VaultClient.ReadSecret(vaultUrl, vaultToken, vaultMountPoint, vaultPath);
            var adminConfig = new AdminClientConfig()
            {
                BootstrapServers = ExtractBootstrapServers(creds),
                SaslMechanism = Confluent.Kafka.SaslMechanism.ScramSha512,
                SecurityProtocol = Confluent.Kafka.SecurityProtocol.SaslPlaintext,
                SaslUsername = ExtractUsername(creds),
                SaslPassword = ExtractPassword(creds)
            };
            return adminConfig;
        }
        private static ProducerConfig GetProducerConfig(string vaultUrl, string vaultToken, string vaultMountPoint, string vaultPath)
        {
            var creds = VaultClient.ReadSecret(vaultUrl, vaultToken, vaultMountPoint, vaultPath);
            var config = new ProducerConfig
            {
                BootstrapServers = ExtractBootstrapServers(creds),
                SaslMechanism = Confluent.Kafka.SaslMechanism.ScramSha512,
                SecurityProtocol = Confluent.Kafka.SecurityProtocol.SaslPlaintext,
                SaslUsername = ExtractUsername(creds),
                SaslPassword = ExtractPassword(creds)
            };
            return config;
        }
        private static ConsumerConfig GetConsumerConfig(string vaultUrl, string vaultToken, string vaultMountPoint, string vaultPath)
        {
            var creds = VaultClient.ReadSecret(vaultUrl, vaultToken, vaultMountPoint, vaultPath);
            var config = new ConsumerConfig
            {
                BootstrapServers = ExtractBootstrapServers(creds),
                SaslMechanism = Confluent.Kafka.SaslMechanism.ScramSha512,
                SecurityProtocol = Confluent.Kafka.SecurityProtocol.SaslPlaintext,
                SaslUsername = ExtractUsername(creds),
                SaslPassword = ExtractPassword(creds),
                /*System.ArgumentException: Message length 16431 exceeds maximum length supported of 4000.*/
                FetchMaxBytes = 47185920,
                MessageMaxBytes = 47185920,
                MaxPartitionFetchBytes = 47185920,
                GroupId = "scout-consumer",
                EnableAutoOffsetStore = false,
                EnableAutoCommit = true,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true,
                // A good introduction to the CooperativeSticky assignor and incremental rebalancing:
                // https://www.confluent.io/blog/cooperative-rebalancing-in-kafka-streams-consumer-ksqldb/
                PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky
            };
            return config;
        }

        private static void ValidateVaultParams(SqlString vaultUrl, SqlString vaultToken, SqlString vaultMountPoint, SqlString vaultPath)
        {
            if (vaultUrl.IsNull || string.IsNullOrWhiteSpace(vaultUrl.Value))
                throw new Exception("@VaultUrl is required.");
            if (vaultToken.IsNull || string.IsNullOrWhiteSpace(vaultToken.Value))
                throw new Exception("@VaultToken is required.");
            if (vaultMountPoint.IsNull || string.IsNullOrWhiteSpace(vaultMountPoint.Value))
                throw new Exception("@VaultMountPoint is required.");
            if (vaultPath.IsNull || string.IsNullOrWhiteSpace(vaultPath.Value))
                throw new Exception("@VaultPath is required.");
        }

        /// <summary>
        /// Все Топики на сервере Kafka
        /// </summary>


[Microsoft.SqlServer.Server.SqlFunction(FillRowMethodName = "GetTopics_FillRow", TableDefinition = "TopicName nvarchar(4000), PartitionsCapacity int, TopicError nvarchar(100)")]
        public static IEnumerable GetTopics(SqlString vaultUrl, SqlString vaultToken, SqlString vaultMountPoint, SqlString vaultPath)
        {
            ValidateVaultParams(vaultUrl, vaultToken, vaultMountPoint, vaultPath);
            var local = new ConcurrentQueue<string>();
            try
            {
                var tableRows = new ArrayList();
                using (var adminClient = new AdminClientBuilder(GetAdminConfig(vaultUrl.Value, vaultToken.Value, vaultMountPoint.Value, vaultPath.Value)).SetLogHandler((client, message) => { local.Enqueue($"{DateTime.Now:HH:mm:ss.fff} [{message.Level}] {message.Facility}: {message.Message}"); }).SetErrorHandler((client, error) => {
                local.Enqueue($"{DateTime.Now:HH:mm:ss.fff} [ERROR] {error.Code}: {error.Reason}");}).Build())
                {
                    var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
                    var topicsMetadata = metadata.Topics;
                    //var topicNames = metadata.Topics.Select(a => a.Topic).ToList();
                    //topicNames.ForEach(n => {
                    //    tableRows.Add(n);
                    //});
                    metadata.Topics.ForEach(topic => {
                        tableRows.Add(topic);
                    });
                }
                return tableRows;
            }
            catch
            {
                while (local.TryDequeue(out var line))
                {
                    SqlContext.Pipe.Send(line);
                }
                throw;
            }
        }
        public static void GetTopics_FillRow(Object obj, out SqlString topicName, out SqlInt32 partitionsCapacity, out SqlString topicError)
        {
            Confluent.Kafka.TopicMetadata topic = (Confluent.Kafka.TopicMetadata)obj;
            topicName = new SqlString(topic.Topic);
            partitionsCapacity = new SqlInt32(topic.Partitions.Count);
            topicError = new SqlString(topic.Error.Reason);
        }
        [Microsoft.SqlServer.Server.SqlProcedure]
        public static void CreateTopic(SqlString topicName, SqlString vaultUrl, SqlString vaultToken, SqlString vaultMountPoint, SqlString vaultPath)
        {
            ValidateVaultParams(vaultUrl, vaultToken, vaultMountPoint, vaultPath);
            if (topicName.ToString().Length > 0)
            {
                using (var adminClient = new AdminClientBuilder(GetAdminConfig(vaultUrl.Value, vaultToken.Value, vaultMountPoint.Value, vaultPath.Value)).Build())
                {
                    try
                    {
                        adminClient.CreateTopicsAsync(new TopicSpecification[] {
                        new TopicSpecification { Name = topicName.ToString(), ReplicationFactor = 3, NumPartitions = 9 } });
                    }
                    catch (CreateTopicsException e)
                    {
                        SqlContext.Pipe.Send($"An error occurred creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
                    }
                }
            }
        }
        [Microsoft.SqlServer.Server.SqlProcedure]
        public static void ProduceMessage(SqlString topicName, SqlString key, SqlString message, SqlString headers, SqlString vaultUrl, SqlString vaultToken, SqlString vaultMountPoint, SqlString vaultPath)
        {
            ValidateVaultParams(vaultUrl, vaultToken, vaultMountPoint, vaultPath);
            using (var producer = new ProducerBuilder<string, string>(GetProducerConfig(vaultUrl.Value, vaultToken.Value, vaultMountPoint.Value, vaultPath.Value)).Build())
            {
                SqlContext.Pipe.Send("\n-----------------------------------------------------------------------");
                SqlContext.Pipe.Send($"Producer {producer.Name} producing on topic {topicName}.");
                SqlContext.Pipe.Send("-----------------------------------------------------------------------");
                try
                {
                    // Создаем объект сообщения
                    var kafkaMessage = new Message<string, string>
                    {
                        Key = key.ToString(),
                        Value = message.ToString(),
                        Headers = new Headers()
                    };
                    // Добавляем headers
                    if (!headers.IsNull && !string.IsNullOrWhiteSpace(headers.Value))
                    {
                        var doc = XDocument.Parse(headers.Value);
                        foreach (var header in doc.Descendants("Header"))
                        {

var name = header.Attribute("Name")?.Value;
                            var value = header.Value;
                            if (!string.IsNullOrEmpty(name))
                            {
                                kafkaMessage.Headers.Add(
                                    name,
                                    Encoding.UTF8.GetBytes(value)
                                );
                            }
                        }
                    }
                    // Асинхронное производство сообщения
                    var deliveryReport = producer.ProduceAsync(
                        topicName.ToString(),
                        kafkaMessage).Result;
                    SqlContext.Pipe.Send($"delivered to: {deliveryReport.TopicPartitionOffset}");
                }
                catch (ProduceException<string, string> e)
                {
                    // Вывод ошибки в лог
                    SqlContext.Pipe.Send($"failed to deliver message: {e.Message} [{e.Error.Code}]");
                    // Явное выбрасывание исключения для SQL Server
                    throw new Exception($"Kafka Produce Error: {e.Error.Reason}", e);
                }
                catch (Exception ex)
                {
                    // Вывод неожиданной ошибки
                    SqlContext.Pipe.Send($"unexpected error: {ex.Message}");
                    // Явное выбрасывание ошибки для обработки SQL Server
                    throw new Exception("An unexpected error occurred while producing a message to Kafka.", ex);
                }
            }
        }
        [Microsoft.SqlServer.Server.SqlProcedure]
        public static void RunConsume(SqlString topicName, SqlString vaultUrl, SqlString vaultToken, SqlString vaultMountPoint, SqlString vaultPath)
        {
            ValidateVaultParams(vaultUrl, vaultToken, vaultMountPoint, vaultPath);
            using (var consumer = new ConsumerBuilder<Ignore, string>(GetConsumerConfig(vaultUrl.Value, vaultToken.Value, vaultMountPoint.Value, vaultPath.Value))
                // Note: All handlers are called on the main .Consume thread.
                .SetErrorHandler((_, e) => SqlContext.Pipe.Send($"Error: {e.Reason}"))
                .SetStatisticsHandler((_, json) => SqlContext.Pipe.Send($"Statistics: {json}"))
                .SetPartitionsAssignedHandler((c, partitions) =>
                {
                    // Since a cooperative assignor (CooperativeSticky) has been configured, the
                    // partition assignment is incremental (adds partitions to any existing assignment).
                    SqlContext.Pipe.Send(
                        "Partitions incrementally assigned: [" +
                        string.Join(",", partitions.Select(p => p.Partition.Value)) +
                        "], all: [" +
                        string.Join(",", c.Assignment.Concat(partitions).Select(p => p.Partition.Value)) +
                        "]");
                    // Possibly manually specify start offsets by returning a list of topic/partition/offsets
                    // to assign to, e.g.:
                    // return partitions.Select(tp => new TopicPartitionOffset(tp, externalOffsets[tp]));
                })
                .SetPartitionsRevokedHandler((c, partitions) =>
                {
                    // Since a cooperative assignor (CooperativeSticky) has been configured, the revoked
                    // assignment is incremental (may remove only some partitions of the current assignment).
                    var remaining = c.Assignment.Where(atp => partitions.Where(rtp => rtp.TopicPartition == atp).Count() == 0);
                    SqlContext.Pipe.Send(
                        "Partitions incrementally revoked: [" +
                        string.Join(",", partitions.Select(p => p.Partition.Value)) +
                        "], remaining: [" +
                        string.Join(",", remaining.Select(p => p.Partition.Value)) +
                        "]");
                })
                .SetPartitionsLostHandler((c, partitions) =>
                {
                    // The lost partitions handler is called when the consumer detects that it has lost ownership
                    // of its assignment (fallen out of the group).

SqlContext.Pipe.Send($"Partitions were lost: [{string.Join(", ", partitions)}]");
                })
                .Build())
            {
                consumer.Subscribe(topicName.ToString());
                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume();
                            if (consumeResult.IsPartitionEOF)
                            {
                                SqlContext.Pipe.Send(
                                    $"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");
                                continue;
                            }
                            SqlContext.Pipe.Send($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");
                            try
                            {
                                // Store the offset associated with consumeResult to a local cache. Stored offsets are committed to Kafka by a background thread every AutoCommitIntervalMs. 
                                // The offset stored is actually the offset of the consumeResult + 1 since by convention, committed offsets specify the next message to consume. 
                                // If EnableAutoOffsetStore had been set to the default value true, the .NET client would automatically store offsets immediately prior to delivering messages to the application. 
                                // Explicitly storing offsets after processing gives at-least once semantics, the default behavior does not.
                                consumer.StoreOffset(consumeResult);
                            }
                            catch (KafkaException e)
                            {
                                SqlContext.Pipe.Send($"Store Offset error: {e.Error.Reason}");
                            }
                        }
                        catch (ConsumeException e)
                        {
                            SqlContext.Pipe.Send($"Consume error: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    SqlContext.Pipe.Send("Closing consumer.");
                    consumer.Close();
                }
            }
        }
    }
}
