// Decompiled with JetBrains decompiler
// Type: Confluent.Kafka.Impl.Librdkafka
// Assembly: Confluent.Kafka, Version=2.3.0.0, Culture=neutral, PublicKeyToken=12c514ca49093d1e
// MVID: CCA09D0F-9A2B-49F0-9914-8EE353DF5887
// Assembly location: C:\Users\antos\Downloads\Confluent.Kafka_nuget.dll

using Confluent.Kafka.Admin;
using Confluent.Kafka.Impl.NativeMethods;
using Confluent.Kafka.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Confluent.Kafka.Impl;

internal static class Librdkafka
{
  private const int RTLD_NOW = 2;
  private const long minVersion = 17105663;
  internal const int MaxErrorStringLength = 512 /*0x0200*/;
  private static object loadLockObj = new object();
  private static bool isInitialized = false;
  private static Func<IntPtr> _version;
  private static Func<IntPtr> _version_str;
  private static Func<IntPtr> _get_debug_contexts;
  private static Func<ErrorCode, IntPtr> _err2str;
  private static Func<IntPtr, IntPtr> _topic_partition_list_new;
  private static Action<IntPtr> _topic_partition_list_destroy;
  private static Func<IntPtr, string, int, IntPtr> _topic_partition_list_add;
  private static Func<IntPtr, IntPtr> _headers_new;
  private static Action<IntPtr> _headers_destroy;
  private static Func<IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, ErrorCode> _header_add;
  private static Librdkafka.headerGetAllDelegate _header_get_all;
  private static Func<ErrorCode> _last_error;
  private static Func<IntPtr, StringBuilder, UIntPtr, ErrorCode> _fatal_error;
  private static Func<IntPtr, IntPtr> _message_errstr;
  private static Librdkafka.messageTimestampDelegate _message_timestamp;
  private static Func<IntPtr, PersistenceStatus> _message_status;
  private static Librdkafka.messageHeadersDelegate _message_headers;
  private static Librdkafka.messageLeaderEpoch _message_leader_epoch;
  private static Action<IntPtr> _message_destroy;
  private static Func<SafeConfigHandle> _conf_new;
  private static Action<IntPtr> _conf_destroy;
  private static Func<IntPtr, IntPtr> _conf_dup;
  private static Func<IntPtr, string, string, StringBuilder, UIntPtr, ConfRes> _conf_set;
  private static Action<IntPtr, Librdkafka.DeliveryReportDelegate> _conf_set_dr_msg_cb;
  private static Action<IntPtr, Librdkafka.RebalanceDelegate> _conf_set_rebalance_cb;
  private static Action<IntPtr, Librdkafka.CommitDelegate> _conf_set_offset_commit_cb;
  private static Action<IntPtr, Librdkafka.ErrorDelegate> _conf_set_error_cb;
  private static Action<IntPtr, Librdkafka.LogDelegate> _conf_set_log_cb;
  private static Action<IntPtr, Librdkafka.StatsDelegate> _conf_set_stats_cb;
  private static Action<IntPtr, Librdkafka.OAuthBearerTokenRefreshDelegate> _conf_set_oauthbearer_token_refresh_cb;
  private static Func<IntPtr, string, long, string, string[], UIntPtr, StringBuilder, UIntPtr, ErrorCode> _oauthbearer_set_token;
  private static Func<IntPtr, string, ErrorCode> _oauthbearer_set_token_failure;
  private static Action<IntPtr, IntPtr> _conf_set_default_topic_conf;
  private static Func<SafeConfigHandle, SafeTopicConfigHandle> _conf_get_default_topic_conf;
  private static Librdkafka.ConfGet _conf_get;
  private static Librdkafka.ConfGet _topic_conf_get;
  private static Librdkafka.ConfDump _conf_dump;
  private static Librdkafka.ConfDump _topic_conf_dump;
  private static Action<IntPtr, UIntPtr> _conf_dump_free;
  private static Func<SafeTopicConfigHandle> _topic_conf_new;
  private static Func<SafeTopicConfigHandle, SafeTopicConfigHandle> _topic_conf_dup;
  private static Func<SafeKafkaHandle, SafeTopicConfigHandle> _default_topic_conf_dup;
  private static Action<IntPtr> _topic_conf_destroy;
  private static Func<IntPtr, string, string, StringBuilder, UIntPtr, ConfRes> _topic_conf_set;
  private static Action<IntPtr, IntPtr> _topic_conf_set_opaque;
  private static Action<IntPtr, Librdkafka.PartitionerDelegate> _topic_conf_set_partitioner_cb;
  private static Func<IntPtr, int, bool> _topic_partition_available;
  private static Func<IntPtr, int> _topic_partition_get_leader_epoch;
  private static Action<IntPtr, int> _topic_partition_set_leader_epoch;
  private static Func<IntPtr, IntPtr, IntPtr> _init_transactions;
  private static Func<IntPtr, IntPtr> _begin_transaction;
  private static Func<IntPtr, IntPtr, IntPtr> _commit_transaction;
  private static Func<IntPtr, IntPtr, IntPtr> _abort_transaction;
  private static Func<IntPtr, IntPtr, IntPtr, IntPtr, IntPtr> _send_offsets_to_transaction;
  private static Func<IntPtr, IntPtr> _rd_kafka_consumer_group_metadata;
  private static Action<IntPtr> _rd_kafka_consumer_group_metadata_destroy;
  private static Librdkafka.ConsumerGroupMetadataWriteDelegate _rd_kafka_consumer_group_metadata_write;
  private static Librdkafka.ConsumerGroupMetadataReadDelegate _rd_kafka_consumer_group_metadata_read;
  private static Func<RdKafkaType, IntPtr, StringBuilder, UIntPtr, SafeKafkaHandle> _new;
  private static Action<IntPtr> _destroy;
  private static Action<IntPtr, IntPtr> _destroy_flags;
  private static Func<IntPtr, IntPtr> _name;
  private static Func<IntPtr, IntPtr> _memberid;
  private static Func<long, long, IntPtr> _Uuid_new;
  private static Func<IntPtr, IntPtr> _Uuid_base64str;
  private static Func<IntPtr, long> _Uuid_most_significant_bits;
  private static Func<IntPtr, long> _Uuid_least_significant_bits;
  private static Action<IntPtr> _Uuid_destroy;
  private static Func<IntPtr, IntPtr, IntPtr, SafeTopicHandle> _topic_new;
  private static Action<IntPtr> _topic_destroy;
  private static Func<IntPtr, IntPtr> _topic_name;
  private static Func<IntPtr, ErrorCode> _poll_set_consumer;
  private static Func<IntPtr, IntPtr, IntPtr> _poll;
  private static Librdkafka.QueryOffsets _query_watermark_offsets;
  private static Librdkafka.GetOffsets _get_watermark_offsets;
  private static Librdkafka.OffsetsForTimes _offsets_for_times;
  private static Action<IntPtr, IntPtr> _mem_free;
  private static Func<IntPtr, IntPtr, ErrorCode> _subscribe;
  private static Func<IntPtr, ErrorCode> _unsubscribe;
  private static Librdkafka.Subscription _subscription;
  private static Func<IntPtr, IntPtr, IntPtr> _consumer_poll;
  private static Func<IntPtr, ErrorCode> _consumer_close;
  private static Func<IntPtr, IntPtr, ErrorCode> _assign;
  private static Func<IntPtr, IntPtr, IntPtr> _incremental_assign;
  private static Func<IntPtr, IntPtr, IntPtr> _incremental_unassign;
  private static Func<IntPtr, IntPtr> _assignment_lost;
  private static Func<IntPtr, IntPtr> _rebalance_protocol;
  private static Librdkafka.Assignment _assignment;
  private static Func<IntPtr, IntPtr, ErrorCode> _offsets_store;
  private static Func<IntPtr, IntPtr, bool, ErrorCode> _commit;
  private static Func<IntPtr, IntPtr, IntPtr, Librdkafka.CommitDelegate, IntPtr, ErrorCode> _commit_queue;
  private static Func<IntPtr, IntPtr, ErrorCode> _pause_partitions;
  private static Func<IntPtr, IntPtr, ErrorCode> _resume_partitions;
  private static Func<IntPtr, int, long, IntPtr, ErrorCode> _seek;
  private static Func<IntPtr, IntPtr, IntPtr, IntPtr> _seek_partitions;
  private static Func<IntPtr, IntPtr, IntPtr, ErrorCode> _committed;
  private static Func<IntPtr, IntPtr, ErrorCode> _position;
  private static Librdkafka.Produceva _produceva;
  private static Librdkafka.Flush _flush;
  private static Librdkafka.Metadata _metadata;
  private static Action<IntPtr> _metadata_destroy;
  private static Librdkafka.ListGroups _list_groups;
  private static Action<IntPtr> _group_list_destroy;
  private static Func<IntPtr, string, IntPtr> _brokers_add;
  private static Librdkafka._sasl_set_credentials_delegate _sasl_set_credentials;
  private static Func<IntPtr, int> _outq_len;
  private static Func<IntPtr, Librdkafka.AdminOp, IntPtr> _AdminOptions_new;
  private static Action<IntPtr> _AdminOptions_destroy;
  private static Func<IntPtr, IntPtr, StringBuilder, UIntPtr, ErrorCode> _AdminOptions_set_request_timeout;
  private static Func<IntPtr, IntPtr, StringBuilder, UIntPtr, ErrorCode> _AdminOptions_set_operation_timeout;
  private static Func<IntPtr, IntPtr, StringBuilder, UIntPtr, ErrorCode> _AdminOptions_set_validate_only;
  private static Func<IntPtr, IntPtr, StringBuilder, UIntPtr, ErrorCode> _AdminOptions_set_incremental;
  private static Func<IntPtr, int, StringBuilder, UIntPtr, ErrorCode> _AdminOptions_set_broker;
  private static Action<IntPtr, IntPtr> _AdminOptions_set_opaque;
  private static Func<IntPtr, IntPtr, IntPtr> _AdminOptions_set_require_stable_offsets;
  private static Func<IntPtr, IntPtr, IntPtr> _AdminOptions_set_include_authorized_operations;
  private static Func<IntPtr, ConsumerGroupState[], UIntPtr, IntPtr> _AdminOptions_set_match_consumer_group_states;
  private static Func<IntPtr, IntPtr, IntPtr> _AdminOptions_set_isolation_level;
  private static Func<string, IntPtr, IntPtr, StringBuilder, UIntPtr, IntPtr> _NewTopic_new;
  private static Action<IntPtr> _NewTopic_destroy;
  private static Func<IntPtr, int, int[], UIntPtr, StringBuilder, UIntPtr, ErrorCode> _NewTopic_set_replica_assignment;
  private static Func<IntPtr, string, string, ErrorCode> _NewTopic_set_config;
  private static Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr> _CreateTopics;
  private static Librdkafka._CreateTopics_result_topics_delegate _CreateTopics_result_topics;
  private static Func<string, IntPtr> _DeleteTopic_new;
  private static Action<IntPtr> _DeleteTopic_destroy;
  private static Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr> _DeleteTopics;
  private static Librdkafka._DeleteTopics_result_topics_delegate _DeleteTopics_result_topics;
  private static Func<string, IntPtr> _DeleteGroup_new;
  private static Action<IntPtr> _DeleteGroup_destroy;
  private static Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr> _DeleteGroups;
  private static Librdkafka._DeleteGroups_result_groups_delegate _DeleteGroups_result_groups;
  private static Func<string, UIntPtr, StringBuilder, UIntPtr, IntPtr> _NewPartitions_new;
  private static Action<IntPtr> _NewPartitions_destroy;
  private static Func<IntPtr, int, int[], UIntPtr, StringBuilder, UIntPtr, ErrorCode> _NewPartitions_set_replica_assignment;
  private static Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr> _CreatePartitions;
  private static Librdkafka._CreatePartitions_result_topics_delegate _CreatePartitions_result_topics;
  private static Func<ConfigSource, IntPtr> _ConfigSource_name;
  private static Func<IntPtr, IntPtr> _ConfigEntry_name;
  private static Func<IntPtr, IntPtr> _ConfigEntry_value;
  private static Func<IntPtr, ConfigSource> _ConfigEntry_source;
  private static Func<IntPtr, IntPtr> _ConfigEntry_is_read_only;
  private static Func<IntPtr, IntPtr> _ConfigEntry_is_default;
  private static Func<IntPtr, IntPtr> _ConfigEntry_is_sensitive;
  private static Func<IntPtr, IntPtr> _ConfigEntry_is_synonym;
  private static Librdkafka._ConfigEntry_synonyms_delegate _ConfigEntry_synonyms;
  private static Func<ResourceType, IntPtr> _ResourceType_name;
  private static Func<ResourceType, string, IntPtr> _ConfigResource_new;
  private static Action<IntPtr> _ConfigResource_destroy;
  private static Func<IntPtr, string, string, ErrorCode> _ConfigResource_add_config;
  private static Func<IntPtr, string, string, ErrorCode> _ConfigResource_set_config;
  private static Func<IntPtr, string, ErrorCode> _ConfigResource_delete_config;
  private static Func<IntPtr, string, AlterConfigOpType, string, IntPtr> _ConfigResource_add_incremental_config;
  private static Librdkafka._ConfigResource_configs_delegate _ConfigResource_configs;
  private static Func<IntPtr, ResourceType> _ConfigResource_type;
  private static Func<IntPtr, IntPtr> _ConfigResource_name;
  private static Func<IntPtr, ErrorCode> _ConfigResource_error;
  private static Func<IntPtr, IntPtr> _ConfigResource_error_string;
  private static Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr> _AlterConfigs;
  private static Librdkafka._AlterConfigs_result_resources_delegate _AlterConfigs_result_resources;
  private static Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr> _IncrementalAlterConfigs;
  private static Librdkafka._IncrementalAlterConfigs_result_resources_delegate _IncrementalAlterConfigs_result_resources;
  private static Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr> _DescribeConfigs;
  private static Librdkafka._DescribeConfigs_result_resources_delegate _DescribeConfigs_result_resources;
  private static Func<IntPtr, IntPtr> _DeleteRecords_new;
  private static Action<IntPtr> _DeleteRecords_destroy;
  private static Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr> _DeleteRecords;
  private static Func<IntPtr, IntPtr> _DeleteRecords_result_offsets;
  private static Func<string, IntPtr, IntPtr> _DeleteConsumerGroupOffsets_new;
  private static Action<IntPtr> _DeleteConsumerGroupOffsets_destroy;
  private static Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr> _DeleteConsumerGroupOffsets;
  private static Librdkafka._DeleteConsumerGroupOffsets_result_groups_delegate _DeleteConsumerGroupOffsets_result_groups;
  private static Librdkafka._AclBinding_new_delegate _AclBinding_new;
  private static Librdkafka._AclBindingFilter_new_delegate _AclBindingFilter_new;
  private static Librdkafka._AclBinding_destroy_delegate _AclBinding_destroy;
  private static Librdkafka._AclBinding_restype_delegate _AclBinding_restype;
  private static Librdkafka._AclBinding_name_delegate _AclBinding_name;
  private static Librdkafka._AclBinding_resource_pattern_type_delegate _AclBinding_resource_pattern_type;
  private static Librdkafka._AclBinding_principal_delegate _AclBinding_principal;
  private static Librdkafka._AclBinding_host_delegate _AclBinding_host;
  private static Librdkafka._AclBinding_operation_delegate _AclBinding_operation;
  private static Librdkafka._AclBinding_permission_type_delegate _AclBinding_permission_type;
  private static Librdkafka._CreateAcls_delegate _CreateAcls;
  private static Librdkafka._CreateAcls_result_acls_delegate _CreateAcls_result_acls;
  private static Librdkafka._acl_result_error_delegate _acl_result_error;
  private static Librdkafka._DescribeAcls_delegate _DescribeAcls;
  private static Librdkafka._DescribeAcls_result_acls_delegate _DescribeAcls_result_acls;
  private static Librdkafka._DeleteAcls_delegate _DeleteAcls;
  private static Librdkafka._DeleteAcls_result_response_error_delegate _DeleteAcls_result_response_error;
  private static Librdkafka._DeleteAcls_result_responses_delegate _DeleteAcls_result_responses;
  private static Librdkafka._DeleteAcls_result_response_matching_acls_delegate _DeleteAcls_result_response_matching_acls;
  private static Librdkafka._AlterConsumerGroupOffsets_new_delegate _AlterConsumerGroupOffsets_new;
  private static Librdkafka._AlterConsumerGroupOffsets_destroy_delegate _AlterConsumerGroupOffsets_destroy;
  private static Librdkafka._AlterConsumerGroupOffsets_delegate _AlterConsumerGroupOffsets;
  private static Librdkafka._AlterConsumerGroupOffsets_result_groups_delegate _AlterConsumerGroupOffsets_result_groups;
  private static Librdkafka._ListConsumerGroupOffsets_new_delegate _ListConsumerGroupOffsets_new;
  private static Librdkafka._ListConsumerGroupOffsets_destroy_delegate _ListConsumerGroupOffsets_destroy;
  private static Librdkafka._ListConsumerGroupOffsets_delegate _ListConsumerGroupOffsets;
  private static Librdkafka._ListConsumerGroupOffsets_result_groups_delegate _ListConsumerGroupOffsets_result_groups;
  private static Librdkafka._ListConsumerGroups_delegate _ListConsumerGroups;
  private static Librdkafka._ConsumerGroupListing_group_id_delegate _ConsumerGroupListing_group_id;
  private static Librdkafka._ConsumerGroupListing_is_simple_consumer_group_delegate _ConsumerGroupListing_is_simple_consumer_group;
  private static Librdkafka._ConsumerGroupListing_state_delegate _ConsumerGroupListing_state;
  private static Librdkafka._ListConsumerGroups_result_valid_delegate _ListConsumerGroups_result_valid;
  private static Librdkafka._ListConsumerGroups_result_errors_delegate _ListConsumerGroups_result_errors;
  private static Librdkafka._DescribeConsumerGroups_delegate _DescribeConsumerGroups;
  private static Librdkafka._DescribeConsumerGroups_result_groups_delegate _DescribeConsumerGroups_result_groups;
  private static Librdkafka._ConsumerGroupDescription_group_id_delegate _ConsumerGroupDescription_group_id;
  private static Librdkafka._ConsumerGroupDescription_error_delegate _ConsumerGroupDescription_error;
  private static Librdkafka._ConsumerGroupDescription_is_simple_consumer_group_delegate _ConsumerGroupDescription_is_simple_consumer_group;
  private static Librdkafka._ConsumerGroupDescription_partition_assignor_delegate _ConsumerGroupDescription_partition_assignor;
  private static Librdkafka._ConsumerGroupDescription_state_delegate _ConsumerGroupDescription_state;
  private static Librdkafka._ConsumerGroupDescription_coordinator_delegate _ConsumerGroupDescription_coordinator;
  private static Librdkafka._ConsumerGroupDescription_member_count_delegate _ConsumerGroupDescription_member_count;
  private static Librdkafka._ConsumerGroupDescription_authorized_operations_delegate _ConsumerGroupDescription_authorized_operations;
  private static Librdkafka._ConsumerGroupDescription_member_delegate _ConsumerGroupDescription_member;
  private static Librdkafka._MemberDescription_client_id_delegate _MemberDescription_client_id;
  private static Librdkafka._MemberDescription_group_instance_id_delegate _MemberDescription_group_instance_id;
  private static Librdkafka._MemberDescription_consumer_id_delegate _MemberDescription_consumer_id;
  private static Librdkafka._MemberDescription_host_delegate _MemberDescription_host;
  private static Librdkafka._MemberDescription_assignment_delegate _MemberDescription_assignment;
  private static Librdkafka._MemberAssignment_partitions_delegate _MemberAssignment_partitions;
  private static Librdkafka._Node_id_delegate _Node_id;
  private static Librdkafka._Node_host_delegate _Node_host;
  private static Librdkafka._Node_port_delegate _Node_port;
  private static Librdkafka._Node_rack_delegate _Node_rack;
  private static Librdkafka._ListOffsets_delegate _ListOffsets;
  private static Librdkafka._ListOffsets_result_infos_delegate _ListOffsets_result_infos;
  private static Librdkafka._ListOffsetsResultInfo_timestamp_delegate _ListOffsetsResultInfo_timestamp;
  private static Librdkafka._ListOffsetsResultInfo_topic_partition_delegate _ListOffsetsResultInfo_topic_partition;
  private static Func<IntPtr, ErrorCode> _topic_result_error;
  private static Func<IntPtr, IntPtr> _topic_result_error_string;
  private static Func<IntPtr, IntPtr> _topic_result_name;
  private static Func<IntPtr, IntPtr> _group_result_name;
  private static Func<IntPtr, IntPtr> _group_result_error;
  private static Func<IntPtr, IntPtr> _group_result_partitions;
  private static Librdkafka._DescribeUserScramCredentials_delegate _DescribeUserScramCredentials;
  private static Librdkafka._DescribeUserScramCredentials_result_descriptions_delegate _DescribeUserScramCredentials_result_descriptions;
  private static Librdkafka._UserScramCredentialsDescription_user_delegate _UserScramCredentialsDescription_user;
  private static Librdkafka._UserScramCredentialsDescription_error_delegate _UserScramCredentialsDescription_error;
  private static Librdkafka._UserScramCredentialsDescription_scramcredentialinfo_count_delegate _UserScramCredentialsDescription_scramcredentialinfo_count;
  private static Librdkafka._UserScramCredentialsDescription_scramcredentialinfo_delegate _UserScramCredentialsDescription_scramcredentialinfo;
  private static Librdkafka._ScramCredentialInfo_mechanism_delegate _ScramCredentialInfo_mechanism;
  private static Librdkafka._ScramCredentialInfo_iterations_delegate _ScramCredentialInfo_iterations;
  private static Librdkafka._UserScramCredentialUpsertion_new_delegate _UserScramCredentialUpsertion_new;
  private static Librdkafka._UserScramCredentialDeletion_new_delegate _UserScramCredentialDeletion_new;
  private static Librdkafka._UserScramCredentialAlteration_destroy_delegate _UserScramCredentialAlteration_destroy;
  private static Librdkafka._AlterUserScramCredentials_delegate _AlterUserScramCredentials;
  private static Librdkafka._AlterUserScramCredentials_result_responses_delegate _AlterUserScramCredentials_result_responses;
  private static Librdkafka._AlterUserScramCredentials_result_response_user_delegate _AlterUserScramCredentials_result_response_user;
  private static Librdkafka._AlterUserScramCredentials_result_response_error_delegate _AlterUserScramCredentials_result_response_error;
  private static Librdkafka._DescribeTopics_delegate _DescribeTopics;
  private static Librdkafka._TopicCollection_of_topic_names_delegate _TopicCollection_of_topic_names;
  private static Librdkafka._TopicCollection_destroy_delegate _TopicCollection_destroy;
  private static Librdkafka._DescribeTopics_result_topics_delegate _DescribeTopics_result_topics;
  private static Librdkafka._TopicDescription_error_delegate _TopicDescription_error;
  private static Librdkafka._TopicDescription_name_delegate _TopicDescription_name;
  private static Librdkafka._TopicDescription_topic_id_delegate _TopicDescription_topic_id;
  private static Librdkafka._TopicDescription_partitions_delegate _TopicDescription_partitions;
  private static Librdkafka._TopicDescription_is_internal_delegate _TopicDescription_is_internal;
  private static Librdkafka._TopicDescription_authorized_operations_delegate _TopicDescription_authorized_operations;
  private static Librdkafka._TopicPartitionInfo_isr_delegate _TopicPartitionInfo_isr;
  private static Librdkafka._TopicPartitionInfo_leader_delegate _TopicPartitionInfo_leader;
  private static Librdkafka._TopicPartitionInfo_partition_delegate _TopicPartitionInfo_partition;
  private static Librdkafka._TopicPartitionInfo_replicas_delegate _TopicPartitionInfo_replicas;
  private static Librdkafka._DescribeCluster_delegate _DescribeCluster;
  private static Librdkafka._DescribeCluster_result_nodes_delegate _DescribeCluster_result_nodes;
  private static Librdkafka._DescribeCluster_result_authorized_operations_delegate _DescribeCluster_result_authorized_operations;
  private static Librdkafka._DescribeCluster_result_controller_delegate _DescribeCluster_result_controller;
  private static Librdkafka._DescribeCluster_result_cluster_id_delegate _DescribeCluster_result_cluster_id;
  private static Func<IntPtr, IntPtr> _queue_new;
  private static Action<IntPtr> _queue_destroy;
  private static Func<IntPtr, IntPtr, IntPtr> _queue_poll;
  private static Action<IntPtr> _event_destroy;
  private static Func<IntPtr, IntPtr> _event_opaque;
  private static Func<IntPtr, Librdkafka.EventType> _event_type;
  private static Func<IntPtr, ErrorCode> _event_error;
  private static Func<IntPtr, IntPtr> _event_error_string;
  private static Func<IntPtr, IntPtr> _event_topic_partition_list;
  private static Func<IntPtr, ErrorCode> _error_code;
  private static Func<IntPtr, IntPtr> _error_string;
  private static Func<IntPtr, IntPtr> _error_is_fatal;
  private static Func<IntPtr, IntPtr> _error_is_retriable;
  private static Func<IntPtr, IntPtr> _error_txn_requires_abort;
  private static Action<IntPtr> _error_destroy;

  private static bool SetDelegates(Type nativeMethodsClass)
  {
    MethodInfo[] array = nativeMethodsClass.GetRuntimeMethods().ToArray<MethodInfo>();
    Librdkafka._version = (Func<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_version")).CreateDelegate(typeof (Func<IntPtr>));
    Librdkafka._version_str = (Func<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_version_str")).CreateDelegate(typeof (Func<IntPtr>));
    Librdkafka._get_debug_contexts = (Func<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_get_debug_contexts")).CreateDelegate(typeof (Func<IntPtr>));
    Librdkafka._err2str = (Func<ErrorCode, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_err2str")).CreateDelegate(typeof (Func<ErrorCode, IntPtr>));
    Librdkafka._last_error = (Func<ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_last_error")).CreateDelegate(typeof (Func<ErrorCode>));
    Librdkafka._fatal_error = (Func<IntPtr, StringBuilder, UIntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_fatal_error")).CreateDelegate(typeof (Func<IntPtr, StringBuilder, UIntPtr, ErrorCode>));
    Librdkafka._message_errstr = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_message_errstr")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._topic_partition_list_new = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_partition_list_new")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._topic_partition_list_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_partition_list_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._topic_partition_list_add = (Func<IntPtr, string, int, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_partition_list_add")).CreateDelegate(typeof (Func<IntPtr, string, int, IntPtr>));
    Librdkafka._headers_new = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_headers_new")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._headers_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_headers_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._header_add = (Func<IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_header_add")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, ErrorCode>));
    Librdkafka._header_get_all = (Librdkafka.headerGetAllDelegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_header_get_all")).CreateDelegate(typeof (Librdkafka.headerGetAllDelegate));
    Librdkafka._message_timestamp = (Librdkafka.messageTimestampDelegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_message_timestamp")).CreateDelegate(typeof (Librdkafka.messageTimestampDelegate));
    Librdkafka._message_headers = (Librdkafka.messageHeadersDelegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_message_headers")).CreateDelegate(typeof (Librdkafka.messageHeadersDelegate));
    Librdkafka._message_status = (Func<IntPtr, PersistenceStatus>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_message_status")).CreateDelegate(typeof (Func<IntPtr, PersistenceStatus>));
    Librdkafka._message_leader_epoch = (Librdkafka.messageLeaderEpoch) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_message_leader_epoch")).CreateDelegate(typeof (Librdkafka.messageLeaderEpoch));
    Librdkafka._message_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_message_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._conf_new = (Func<SafeConfigHandle>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_new")).CreateDelegate(typeof (Func<SafeConfigHandle>));
    Librdkafka._conf_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._conf_dup = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_dup")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._conf_set = (Func<IntPtr, string, string, StringBuilder, UIntPtr, ConfRes>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_set")).CreateDelegate(typeof (Func<IntPtr, string, string, StringBuilder, UIntPtr, ConfRes>));
    Librdkafka._conf_set_dr_msg_cb = (Action<IntPtr, Librdkafka.DeliveryReportDelegate>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_set_dr_msg_cb")).CreateDelegate(typeof (Action<IntPtr, Librdkafka.DeliveryReportDelegate>));
    Librdkafka._conf_set_rebalance_cb = (Action<IntPtr, Librdkafka.RebalanceDelegate>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_set_rebalance_cb")).CreateDelegate(typeof (Action<IntPtr, Librdkafka.RebalanceDelegate>));
    Librdkafka._conf_set_error_cb = (Action<IntPtr, Librdkafka.ErrorDelegate>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_set_error_cb")).CreateDelegate(typeof (Action<IntPtr, Librdkafka.ErrorDelegate>));
    Librdkafka._conf_set_offset_commit_cb = (Action<IntPtr, Librdkafka.CommitDelegate>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_set_offset_commit_cb")).CreateDelegate(typeof (Action<IntPtr, Librdkafka.CommitDelegate>));
    Librdkafka._conf_set_log_cb = (Action<IntPtr, Librdkafka.LogDelegate>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_set_log_cb")).CreateDelegate(typeof (Action<IntPtr, Librdkafka.LogDelegate>));
    Librdkafka._conf_set_stats_cb = (Action<IntPtr, Librdkafka.StatsDelegate>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_set_stats_cb")).CreateDelegate(typeof (Action<IntPtr, Librdkafka.StatsDelegate>));
    Librdkafka._conf_set_oauthbearer_token_refresh_cb = (Action<IntPtr, Librdkafka.OAuthBearerTokenRefreshDelegate>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_set_oauthbearer_token_refresh_cb")).CreateDelegate(typeof (Action<IntPtr, Librdkafka.OAuthBearerTokenRefreshDelegate>));
    Librdkafka._oauthbearer_set_token = (Func<IntPtr, string, long, string, string[], UIntPtr, StringBuilder, UIntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_oauthbearer_set_token")).CreateDelegate(typeof (Func<IntPtr, string, long, string, string[], UIntPtr, StringBuilder, UIntPtr, ErrorCode>));
    Librdkafka._oauthbearer_set_token_failure = (Func<IntPtr, string, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_oauthbearer_set_token_failure")).CreateDelegate(typeof (Func<IntPtr, string, ErrorCode>));
    Librdkafka._conf_set_default_topic_conf = (Action<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_set_default_topic_conf")).CreateDelegate(typeof (Action<IntPtr, IntPtr>));
    Librdkafka._conf_get_default_topic_conf = (Func<SafeConfigHandle, SafeTopicConfigHandle>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_get_default_topic_conf")).CreateDelegate(typeof (Func<SafeConfigHandle, SafeTopicConfigHandle>));
    Librdkafka._conf_get = (Librdkafka.ConfGet) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_get")).CreateDelegate(typeof (Librdkafka.ConfGet));
    Librdkafka._topic_conf_get = (Librdkafka.ConfGet) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_conf_get")).CreateDelegate(typeof (Librdkafka.ConfGet));
    Librdkafka._conf_dump = (Librdkafka.ConfDump) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_dump")).CreateDelegate(typeof (Librdkafka.ConfDump));
    Librdkafka._topic_conf_dump = (Librdkafka.ConfDump) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_conf_dump")).CreateDelegate(typeof (Librdkafka.ConfDump));
    Librdkafka._conf_dump_free = (Action<IntPtr, UIntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_conf_dump_free")).CreateDelegate(typeof (Action<IntPtr, UIntPtr>));
    Librdkafka._topic_conf_new = (Func<SafeTopicConfigHandle>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_conf_new")).CreateDelegate(typeof (Func<SafeTopicConfigHandle>));
    Librdkafka._topic_conf_dup = (Func<SafeTopicConfigHandle, SafeTopicConfigHandle>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_conf_dup")).CreateDelegate(typeof (Func<SafeTopicConfigHandle, SafeTopicConfigHandle>));
    Librdkafka._default_topic_conf_dup = (Func<SafeKafkaHandle, SafeTopicConfigHandle>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_default_topic_conf_dup")).CreateDelegate(typeof (Func<SafeKafkaHandle, SafeTopicConfigHandle>));
    Librdkafka._topic_conf_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_conf_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._topic_conf_set = (Func<IntPtr, string, string, StringBuilder, UIntPtr, ConfRes>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_conf_set")).CreateDelegate(typeof (Func<IntPtr, string, string, StringBuilder, UIntPtr, ConfRes>));
    Librdkafka._topic_conf_set_partitioner_cb = (Action<IntPtr, Librdkafka.PartitionerDelegate>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_conf_set_partitioner_cb")).CreateDelegate(typeof (Action<IntPtr, Librdkafka.PartitionerDelegate>));
    Librdkafka._topic_conf_set_opaque = (Action<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_conf_set_opaque")).CreateDelegate(typeof (Action<IntPtr, IntPtr>));
    Librdkafka._topic_partition_available = (Func<IntPtr, int, bool>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_partition_available")).CreateDelegate(typeof (Func<IntPtr, int, bool>));
    Librdkafka._topic_partition_get_leader_epoch = (Func<IntPtr, int>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_partition_get_leader_epoch")).CreateDelegate(typeof (Func<IntPtr, int>));
    Librdkafka._topic_partition_set_leader_epoch = (Action<IntPtr, int>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_partition_set_leader_epoch")).CreateDelegate(typeof (Action<IntPtr, int>));
    Librdkafka._init_transactions = (Func<IntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_init_transactions")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr>));
    Librdkafka._begin_transaction = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_begin_transaction")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._commit_transaction = (Func<IntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_commit_transaction")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr>));
    Librdkafka._abort_transaction = (Func<IntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_abort_transaction")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr>));
    Librdkafka._send_offsets_to_transaction = (Func<IntPtr, IntPtr, IntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_send_offsets_to_transaction")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr, IntPtr, IntPtr>));
    Librdkafka._rd_kafka_consumer_group_metadata = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_consumer_group_metadata")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._rd_kafka_consumer_group_metadata_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_consumer_group_metadata_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._rd_kafka_consumer_group_metadata_write = (Librdkafka.ConsumerGroupMetadataWriteDelegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_consumer_group_metadata_write")).CreateDelegate(typeof (Librdkafka.ConsumerGroupMetadataWriteDelegate));
    Librdkafka._rd_kafka_consumer_group_metadata_read = (Librdkafka.ConsumerGroupMetadataReadDelegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_consumer_group_metadata_read")).CreateDelegate(typeof (Librdkafka.ConsumerGroupMetadataReadDelegate));
    Librdkafka._new = (Func<RdKafkaType, IntPtr, StringBuilder, UIntPtr, SafeKafkaHandle>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_new")).CreateDelegate(typeof (Func<RdKafkaType, IntPtr, StringBuilder, UIntPtr, SafeKafkaHandle>));
    Librdkafka._name = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_name")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._memberid = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_memberid")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._Uuid_new = (Func<long, long, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_Uuid_new")).CreateDelegate(typeof (Func<long, long, IntPtr>));
    Librdkafka._Uuid_base64str = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_Uuid_base64str")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._Uuid_most_significant_bits = (Func<IntPtr, long>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_Uuid_most_significant_bits")).CreateDelegate(typeof (Func<IntPtr, long>));
    Librdkafka._Uuid_least_significant_bits = (Func<IntPtr, long>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_Uuid_least_significant_bits")).CreateDelegate(typeof (Func<IntPtr, long>));
    Librdkafka._Uuid_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_Uuid_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._topic_new = (Func<IntPtr, IntPtr, IntPtr, SafeTopicHandle>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_new")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr, SafeTopicHandle>));
    Librdkafka._topic_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._topic_name = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_name")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._poll = (Func<IntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_poll")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr>));
    Librdkafka._poll_set_consumer = (Func<IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_poll_set_consumer")).CreateDelegate(typeof (Func<IntPtr, ErrorCode>));
    Librdkafka._query_watermark_offsets = (Librdkafka.QueryOffsets) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_query_watermark_offsets")).CreateDelegate(typeof (Librdkafka.QueryOffsets));
    Librdkafka._get_watermark_offsets = (Librdkafka.GetOffsets) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_get_watermark_offsets")).CreateDelegate(typeof (Librdkafka.GetOffsets));
    Librdkafka._offsets_for_times = (Librdkafka.OffsetsForTimes) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_offsets_for_times")).CreateDelegate(typeof (Librdkafka.OffsetsForTimes));
    Librdkafka._mem_free = (Action<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_mem_free")).CreateDelegate(typeof (Action<IntPtr, IntPtr>));
    Librdkafka._subscribe = (Func<IntPtr, IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_subscribe")).CreateDelegate(typeof (Func<IntPtr, IntPtr, ErrorCode>));
    Librdkafka._unsubscribe = (Func<IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_unsubscribe")).CreateDelegate(typeof (Func<IntPtr, ErrorCode>));
    Librdkafka._subscription = (Librdkafka.Subscription) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_subscription")).CreateDelegate(typeof (Librdkafka.Subscription));
    Librdkafka._consumer_poll = (Func<IntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_consumer_poll")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr>));
    Librdkafka._consumer_close = (Func<IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_consumer_close")).CreateDelegate(typeof (Func<IntPtr, ErrorCode>));
    Librdkafka._assign = (Func<IntPtr, IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_assign")).CreateDelegate(typeof (Func<IntPtr, IntPtr, ErrorCode>));
    Librdkafka._incremental_assign = (Func<IntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_incremental_assign")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr>));
    Librdkafka._incremental_unassign = (Func<IntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_incremental_unassign")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr>));
    Librdkafka._assignment_lost = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_assignment_lost")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._rebalance_protocol = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_rebalance_protocol")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._assignment = (Librdkafka.Assignment) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_assignment")).CreateDelegate(typeof (Librdkafka.Assignment));
    Librdkafka._offsets_store = (Func<IntPtr, IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_offsets_store")).CreateDelegate(typeof (Func<IntPtr, IntPtr, ErrorCode>));
    Librdkafka._commit = (Func<IntPtr, IntPtr, bool, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_commit")).CreateDelegate(typeof (Func<IntPtr, IntPtr, bool, ErrorCode>));
    Librdkafka._commit_queue = (Func<IntPtr, IntPtr, IntPtr, Librdkafka.CommitDelegate, IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_commit_queue")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr, Librdkafka.CommitDelegate, IntPtr, ErrorCode>));
    Librdkafka._committed = (Func<IntPtr, IntPtr, IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_committed")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr, ErrorCode>));
    Librdkafka._pause_partitions = (Func<IntPtr, IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_pause_partitions")).CreateDelegate(typeof (Func<IntPtr, IntPtr, ErrorCode>));
    Librdkafka._resume_partitions = (Func<IntPtr, IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_resume_partitions")).CreateDelegate(typeof (Func<IntPtr, IntPtr, ErrorCode>));
    Librdkafka._seek = (Func<IntPtr, int, long, IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_seek")).CreateDelegate(typeof (Func<IntPtr, int, long, IntPtr, ErrorCode>));
    Librdkafka._seek_partitions = (Func<IntPtr, IntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_seek_partitions")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr, IntPtr>));
    Librdkafka._position = (Func<IntPtr, IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_position")).CreateDelegate(typeof (Func<IntPtr, IntPtr, ErrorCode>));
    Librdkafka._produceva = (Librdkafka.Produceva) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_produceva")).CreateDelegate(typeof (Librdkafka.Produceva));
    Librdkafka._flush = (Librdkafka.Flush) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_flush")).CreateDelegate(typeof (Librdkafka.Flush));
    Librdkafka._metadata = (Librdkafka.Metadata) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_metadata")).CreateDelegate(typeof (Librdkafka.Metadata));
    Librdkafka._metadata_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_metadata_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._list_groups = (Librdkafka.ListGroups) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_list_groups")).CreateDelegate(typeof (Librdkafka.ListGroups));
    Librdkafka._group_list_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_group_list_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._brokers_add = (Func<IntPtr, string, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_brokers_add")).CreateDelegate(typeof (Func<IntPtr, string, IntPtr>));
    Librdkafka._sasl_set_credentials = (Librdkafka._sasl_set_credentials_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_sasl_set_credentials")).CreateDelegate(typeof (Librdkafka._sasl_set_credentials_delegate));
    Librdkafka._outq_len = (Func<IntPtr, int>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_outq_len")).CreateDelegate(typeof (Func<IntPtr, int>));
    Librdkafka._queue_new = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_queue_new")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._queue_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_queue_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._event_opaque = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_event_opaque")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._event_type = (Func<IntPtr, Librdkafka.EventType>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_event_type")).CreateDelegate(typeof (Func<IntPtr, Librdkafka.EventType>));
    Librdkafka._event_error = (Func<IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_event_error")).CreateDelegate(typeof (Func<IntPtr, ErrorCode>));
    Librdkafka._event_error_string = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_event_error_string")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._event_topic_partition_list = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_event_topic_partition_list")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._event_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_event_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._queue_poll = (Func<IntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_queue_poll")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr>));
    Librdkafka._AdminOptions_new = (Func<IntPtr, Librdkafka.AdminOp, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AdminOptions_new")).CreateDelegate(typeof (Func<IntPtr, Librdkafka.AdminOp, IntPtr>));
    Librdkafka._AdminOptions_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AdminOptions_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._AdminOptions_set_request_timeout = (Func<IntPtr, IntPtr, StringBuilder, UIntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AdminOptions_set_request_timeout")).CreateDelegate(typeof (Func<IntPtr, IntPtr, StringBuilder, UIntPtr, ErrorCode>));
    Librdkafka._AdminOptions_set_operation_timeout = (Func<IntPtr, IntPtr, StringBuilder, UIntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AdminOptions_set_operation_timeout")).CreateDelegate(typeof (Func<IntPtr, IntPtr, StringBuilder, UIntPtr, ErrorCode>));
    Librdkafka._AdminOptions_set_validate_only = (Func<IntPtr, IntPtr, StringBuilder, UIntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AdminOptions_set_validate_only")).CreateDelegate(typeof (Func<IntPtr, IntPtr, StringBuilder, UIntPtr, ErrorCode>));
    Librdkafka._AdminOptions_set_incremental = (Func<IntPtr, IntPtr, StringBuilder, UIntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AdminOptions_set_incremental")).CreateDelegate(typeof (Func<IntPtr, IntPtr, StringBuilder, UIntPtr, ErrorCode>));
    Librdkafka._AdminOptions_set_broker = (Func<IntPtr, int, StringBuilder, UIntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AdminOptions_set_broker")).CreateDelegate(typeof (Func<IntPtr, int, StringBuilder, UIntPtr, ErrorCode>));
    Librdkafka._AdminOptions_set_opaque = (Action<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AdminOptions_set_opaque")).CreateDelegate(typeof (Action<IntPtr, IntPtr>));
    Librdkafka._AdminOptions_set_require_stable_offsets = (Func<IntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AdminOptions_set_require_stable_offsets")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr>));
    Librdkafka._AdminOptions_set_include_authorized_operations = (Func<IntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AdminOptions_set_include_authorized_operations")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr>));
    Librdkafka._AdminOptions_set_match_consumer_group_states = (Func<IntPtr, ConsumerGroupState[], UIntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AdminOptions_set_match_consumer_group_states")).CreateDelegate(typeof (Func<IntPtr, ConsumerGroupState[], UIntPtr, IntPtr>));
    Librdkafka._AdminOptions_set_isolation_level = (Func<IntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AdminOptions_set_isolation_level")).CreateDelegate(typeof (Func<IntPtr, IntPtr, IntPtr>));
    Librdkafka._NewTopic_new = (Func<string, IntPtr, IntPtr, StringBuilder, UIntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_NewTopic_new")).CreateDelegate(typeof (Func<string, IntPtr, IntPtr, StringBuilder, UIntPtr, IntPtr>));
    Librdkafka._NewTopic_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_NewTopic_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._NewTopic_set_replica_assignment = (Func<IntPtr, int, int[], UIntPtr, StringBuilder, UIntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_NewTopic_set_replica_assignment")).CreateDelegate(typeof (Func<IntPtr, int, int[], UIntPtr, StringBuilder, UIntPtr, ErrorCode>));
    Librdkafka._NewTopic_set_config = (Func<IntPtr, string, string, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_NewTopic_set_config")).CreateDelegate(typeof (Func<IntPtr, string, string, ErrorCode>));
    Librdkafka._CreateTopics = (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_CreateTopics")).CreateDelegate(typeof (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>));
    Librdkafka._CreateTopics_result_topics = (Librdkafka._CreateTopics_result_topics_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_CreateTopics_result_topics")).CreateDelegate(typeof (Librdkafka._CreateTopics_result_topics_delegate));
    Librdkafka._DeleteTopic_new = (Func<string, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteTopic_new")).CreateDelegate(typeof (Func<string, IntPtr>));
    Librdkafka._DeleteTopic_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteTopic_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._DeleteTopics = (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteTopics")).CreateDelegate(typeof (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>));
    Librdkafka._DeleteTopics_result_topics = (Librdkafka._DeleteTopics_result_topics_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteTopics_result_topics")).CreateDelegate(typeof (Librdkafka._DeleteTopics_result_topics_delegate));
    Librdkafka._DeleteGroup_new = (Func<string, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteGroup_new")).CreateDelegate(typeof (Func<string, IntPtr>));
    Librdkafka._DeleteGroup_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteGroup_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._DeleteGroups = (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteGroups")).CreateDelegate(typeof (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>));
    Librdkafka._DeleteGroups_result_groups = (Librdkafka._DeleteGroups_result_groups_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteGroups_result_groups")).CreateDelegate(typeof (Librdkafka._DeleteGroups_result_groups_delegate));
    Librdkafka._DeleteRecords_new = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteRecords_new")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._DeleteRecords_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteRecords_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._DeleteRecords = (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteRecords")).CreateDelegate(typeof (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>));
    Librdkafka._DeleteRecords_result_offsets = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteRecords_result_offsets")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._DeleteConsumerGroupOffsets_new = (Func<string, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteConsumerGroupOffsets_new")).CreateDelegate(typeof (Func<string, IntPtr, IntPtr>));
    Librdkafka._DeleteConsumerGroupOffsets_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteConsumerGroupOffsets_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._DeleteConsumerGroupOffsets = (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteConsumerGroupOffsets")).CreateDelegate(typeof (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>));
    Librdkafka._DeleteConsumerGroupOffsets_result_groups = (Librdkafka._DeleteConsumerGroupOffsets_result_groups_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteConsumerGroupOffsets_result_groups")).CreateDelegate(typeof (Librdkafka._DeleteConsumerGroupOffsets_result_groups_delegate));
    Librdkafka._NewPartitions_new = (Func<string, UIntPtr, StringBuilder, UIntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_NewPartitions_new")).CreateDelegate(typeof (Func<string, UIntPtr, StringBuilder, UIntPtr, IntPtr>));
    Librdkafka._NewPartitions_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_NewPartitions_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._NewPartitions_set_replica_assignment = (Func<IntPtr, int, int[], UIntPtr, StringBuilder, UIntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_NewPartitions_set_replica_assignment")).CreateDelegate(typeof (Func<IntPtr, int, int[], UIntPtr, StringBuilder, UIntPtr, ErrorCode>));
    Librdkafka._CreatePartitions = (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_CreatePartitions")).CreateDelegate(typeof (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>));
    Librdkafka._CreatePartitions_result_topics = (Librdkafka._CreatePartitions_result_topics_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_CreatePartitions_result_topics")).CreateDelegate(typeof (Librdkafka._CreatePartitions_result_topics_delegate));
    Librdkafka._ConfigSource_name = (Func<ConfigSource, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigSource_name")).CreateDelegate(typeof (Func<ConfigSource, IntPtr>));
    Librdkafka._ConfigEntry_name = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigEntry_name")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._ConfigEntry_value = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigEntry_value")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._ConfigEntry_source = (Func<IntPtr, ConfigSource>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigEntry_source")).CreateDelegate(typeof (Func<IntPtr, ConfigSource>));
    Librdkafka._ConfigEntry_is_read_only = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigEntry_is_read_only")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._ConfigEntry_is_default = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigEntry_is_default")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._ConfigEntry_is_sensitive = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigEntry_is_sensitive")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._ConfigEntry_is_synonym = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigEntry_is_synonym")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._ConfigEntry_synonyms = (Librdkafka._ConfigEntry_synonyms_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigEntry_synonyms")).CreateDelegate(typeof (Librdkafka._ConfigEntry_synonyms_delegate));
    Librdkafka._ResourceType_name = (Func<ResourceType, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ResourceType_name")).CreateDelegate(typeof (Func<ResourceType, IntPtr>));
    Librdkafka._ConfigResource_new = (Func<ResourceType, string, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigResource_new")).CreateDelegate(typeof (Func<ResourceType, string, IntPtr>));
    Librdkafka._ConfigResource_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigResource_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._ConfigResource_add_config = (Func<IntPtr, string, string, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigResource_add_config")).CreateDelegate(typeof (Func<IntPtr, string, string, ErrorCode>));
    Librdkafka._ConfigResource_set_config = (Func<IntPtr, string, string, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigResource_set_config")).CreateDelegate(typeof (Func<IntPtr, string, string, ErrorCode>));
    Librdkafka._ConfigResource_delete_config = (Func<IntPtr, string, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigResource_delete_config")).CreateDelegate(typeof (Func<IntPtr, string, ErrorCode>));
    Librdkafka._ConfigResource_add_incremental_config = (Func<IntPtr, string, AlterConfigOpType, string, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigResource_add_incremental_config")).CreateDelegate(typeof (Func<IntPtr, string, AlterConfigOpType, string, IntPtr>));
    Librdkafka._ConfigResource_configs = (Librdkafka._ConfigResource_configs_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigResource_configs")).CreateDelegate(typeof (Librdkafka._ConfigResource_configs_delegate));
    Librdkafka._ConfigResource_type = (Func<IntPtr, ResourceType>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigResource_type")).CreateDelegate(typeof (Func<IntPtr, ResourceType>));
    Librdkafka._ConfigResource_name = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigResource_name")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._ConfigResource_error = (Func<IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigResource_error")).CreateDelegate(typeof (Func<IntPtr, ErrorCode>));
    Librdkafka._ConfigResource_error_string = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConfigResource_error_string")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._AlterConfigs = (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AlterConfigs")).CreateDelegate(typeof (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>));
    Librdkafka._AlterConfigs_result_resources = (Librdkafka._AlterConfigs_result_resources_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AlterConfigs_result_resources")).CreateDelegate(typeof (Librdkafka._AlterConfigs_result_resources_delegate));
    Librdkafka._IncrementalAlterConfigs = (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_IncrementalAlterConfigs")).CreateDelegate(typeof (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>));
    Librdkafka._IncrementalAlterConfigs_result_resources = (Librdkafka._IncrementalAlterConfigs_result_resources_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_IncrementalAlterConfigs_result_resources")).CreateDelegate(typeof (Librdkafka._IncrementalAlterConfigs_result_resources_delegate));
    Librdkafka._DescribeConfigs = (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeConfigs")).CreateDelegate(typeof (Action<IntPtr, IntPtr[], UIntPtr, IntPtr, IntPtr>));
    Librdkafka._DescribeConfigs_result_resources = (Librdkafka._DescribeConfigs_result_resources_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeConfigs_result_resources")).CreateDelegate(typeof (Librdkafka._DescribeConfigs_result_resources_delegate));
    Librdkafka._AclBinding_new = (Librdkafka._AclBinding_new_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AclBinding_new")).CreateDelegate(typeof (Librdkafka._AclBinding_new_delegate));
    Librdkafka._AclBindingFilter_new = (Librdkafka._AclBindingFilter_new_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AclBindingFilter_new")).CreateDelegate(typeof (Librdkafka._AclBindingFilter_new_delegate));
    Librdkafka._AclBinding_destroy = (Librdkafka._AclBinding_destroy_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AclBinding_destroy")).CreateDelegate(typeof (Librdkafka._AclBinding_destroy_delegate));
    Librdkafka._AclBinding_restype = (Librdkafka._AclBinding_restype_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AclBinding_restype")).CreateDelegate(typeof (Librdkafka._AclBinding_restype_delegate));
    Librdkafka._AclBinding_name = (Librdkafka._AclBinding_name_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AclBinding_name")).CreateDelegate(typeof (Librdkafka._AclBinding_name_delegate));
    Librdkafka._AclBinding_resource_pattern_type = (Librdkafka._AclBinding_resource_pattern_type_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AclBinding_resource_pattern_type")).CreateDelegate(typeof (Librdkafka._AclBinding_resource_pattern_type_delegate));
    Librdkafka._AclBinding_principal = (Librdkafka._AclBinding_principal_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AclBinding_principal")).CreateDelegate(typeof (Librdkafka._AclBinding_principal_delegate));
    Librdkafka._AclBinding_host = (Librdkafka._AclBinding_host_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AclBinding_host")).CreateDelegate(typeof (Librdkafka._AclBinding_host_delegate));
    Librdkafka._AclBinding_operation = (Librdkafka._AclBinding_operation_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AclBinding_operation")).CreateDelegate(typeof (Librdkafka._AclBinding_operation_delegate));
    Librdkafka._AclBinding_permission_type = (Librdkafka._AclBinding_permission_type_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AclBinding_permission_type")).CreateDelegate(typeof (Librdkafka._AclBinding_permission_type_delegate));
    Librdkafka._CreateAcls = (Librdkafka._CreateAcls_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_CreateAcls")).CreateDelegate(typeof (Librdkafka._CreateAcls_delegate));
    Librdkafka._CreateAcls_result_acls = (Librdkafka._CreateAcls_result_acls_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_CreateAcls_result_acls")).CreateDelegate(typeof (Librdkafka._CreateAcls_result_acls_delegate));
    Librdkafka._acl_result_error = (Librdkafka._acl_result_error_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_acl_result_error")).CreateDelegate(typeof (Librdkafka._acl_result_error_delegate));
    Librdkafka._DescribeAcls = (Librdkafka._DescribeAcls_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeAcls")).CreateDelegate(typeof (Librdkafka._DescribeAcls_delegate));
    Librdkafka._DescribeAcls_result_acls = (Librdkafka._DescribeAcls_result_acls_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeAcls_result_acls")).CreateDelegate(typeof (Librdkafka._DescribeAcls_result_acls_delegate));
    Librdkafka._DeleteAcls = (Librdkafka._DeleteAcls_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteAcls")).CreateDelegate(typeof (Librdkafka._DeleteAcls_delegate));
    Librdkafka._DeleteAcls_result_responses = (Librdkafka._DeleteAcls_result_responses_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteAcls_result_responses")).CreateDelegate(typeof (Librdkafka._DeleteAcls_result_responses_delegate));
    Librdkafka._DeleteAcls_result_response_error = (Librdkafka._DeleteAcls_result_response_error_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteAcls_result_response_error")).CreateDelegate(typeof (Librdkafka._DeleteAcls_result_response_error_delegate));
    Librdkafka._DeleteAcls_result_response_matching_acls = (Librdkafka._DeleteAcls_result_response_matching_acls_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DeleteAcls_result_response_matching_acls")).CreateDelegate(typeof (Librdkafka._DeleteAcls_result_response_matching_acls_delegate));
    Librdkafka._AlterConsumerGroupOffsets_new = (Librdkafka._AlterConsumerGroupOffsets_new_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AlterConsumerGroupOffsets_new")).CreateDelegate(typeof (Librdkafka._AlterConsumerGroupOffsets_new_delegate));
    Librdkafka._AlterConsumerGroupOffsets_destroy = (Librdkafka._AlterConsumerGroupOffsets_destroy_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AlterConsumerGroupOffsets_destroy")).CreateDelegate(typeof (Librdkafka._AlterConsumerGroupOffsets_destroy_delegate));
    Librdkafka._AlterConsumerGroupOffsets_result_groups = (Librdkafka._AlterConsumerGroupOffsets_result_groups_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AlterConsumerGroupOffsets_result_groups")).CreateDelegate(typeof (Librdkafka._AlterConsumerGroupOffsets_result_groups_delegate));
    Librdkafka._AlterConsumerGroupOffsets = (Librdkafka._AlterConsumerGroupOffsets_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AlterConsumerGroupOffsets")).CreateDelegate(typeof (Librdkafka._AlterConsumerGroupOffsets_delegate));
    Librdkafka._ListConsumerGroupOffsets_new = (Librdkafka._ListConsumerGroupOffsets_new_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ListConsumerGroupOffsets_new")).CreateDelegate(typeof (Librdkafka._ListConsumerGroupOffsets_new_delegate));
    Librdkafka._ListConsumerGroupOffsets_destroy = (Librdkafka._ListConsumerGroupOffsets_destroy_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ListConsumerGroupOffsets_destroy")).CreateDelegate(typeof (Librdkafka._ListConsumerGroupOffsets_destroy_delegate));
    Librdkafka._ListConsumerGroupOffsets_result_groups = (Librdkafka._ListConsumerGroupOffsets_result_groups_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ListConsumerGroupOffsets_result_groups")).CreateDelegate(typeof (Librdkafka._ListConsumerGroupOffsets_result_groups_delegate));
    Librdkafka._ListConsumerGroupOffsets = (Librdkafka._ListConsumerGroupOffsets_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ListConsumerGroupOffsets")).CreateDelegate(typeof (Librdkafka._ListConsumerGroupOffsets_delegate));
    Librdkafka._ListConsumerGroups = (Librdkafka._ListConsumerGroups_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ListConsumerGroups")).CreateDelegate(typeof (Librdkafka._ListConsumerGroups_delegate));
    Librdkafka._ConsumerGroupListing_group_id = (Librdkafka._ConsumerGroupListing_group_id_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConsumerGroupListing_group_id")).CreateDelegate(typeof (Librdkafka._ConsumerGroupListing_group_id_delegate));
    Librdkafka._ConsumerGroupListing_is_simple_consumer_group = (Librdkafka._ConsumerGroupListing_is_simple_consumer_group_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConsumerGroupListing_is_simple_consumer_group")).CreateDelegate(typeof (Librdkafka._ConsumerGroupListing_is_simple_consumer_group_delegate));
    Librdkafka._ConsumerGroupListing_state = (Librdkafka._ConsumerGroupListing_state_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConsumerGroupListing_state")).CreateDelegate(typeof (Librdkafka._ConsumerGroupListing_state_delegate));
    Librdkafka._ListConsumerGroups_result_valid = (Librdkafka._ListConsumerGroups_result_valid_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ListConsumerGroups_result_valid")).CreateDelegate(typeof (Librdkafka._ListConsumerGroups_result_valid_delegate));
    Librdkafka._ListConsumerGroups_result_errors = (Librdkafka._ListConsumerGroups_result_errors_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ListConsumerGroups_result_errors")).CreateDelegate(typeof (Librdkafka._ListConsumerGroups_result_errors_delegate));
    Librdkafka._DescribeConsumerGroups = (Librdkafka._DescribeConsumerGroups_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeConsumerGroups")).CreateDelegate(typeof (Librdkafka._DescribeConsumerGroups_delegate));
    Librdkafka._DescribeConsumerGroups_result_groups = (Librdkafka._DescribeConsumerGroups_result_groups_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeConsumerGroups_result_groups")).CreateDelegate(typeof (Librdkafka._DescribeConsumerGroups_result_groups_delegate));
    Librdkafka._ConsumerGroupDescription_group_id = (Librdkafka._ConsumerGroupDescription_group_id_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConsumerGroupDescription_group_id")).CreateDelegate(typeof (Librdkafka._ConsumerGroupDescription_group_id_delegate));
    Librdkafka._ConsumerGroupDescription_error = (Librdkafka._ConsumerGroupDescription_error_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConsumerGroupDescription_error")).CreateDelegate(typeof (Librdkafka._ConsumerGroupDescription_error_delegate));
    Librdkafka._ConsumerGroupDescription_is_simple_consumer_group = (Librdkafka._ConsumerGroupDescription_is_simple_consumer_group_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConsumerGroupDescription_is_simple_consumer_group")).CreateDelegate(typeof (Librdkafka._ConsumerGroupDescription_is_simple_consumer_group_delegate));
    Librdkafka._ConsumerGroupDescription_partition_assignor = (Librdkafka._ConsumerGroupDescription_partition_assignor_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConsumerGroupDescription_partition_assignor")).CreateDelegate(typeof (Librdkafka._ConsumerGroupDescription_partition_assignor_delegate));
    Librdkafka._ConsumerGroupDescription_state = (Librdkafka._ConsumerGroupDescription_state_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConsumerGroupDescription_state")).CreateDelegate(typeof (Librdkafka._ConsumerGroupDescription_state_delegate));
    Librdkafka._ConsumerGroupDescription_coordinator = (Librdkafka._ConsumerGroupDescription_coordinator_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConsumerGroupDescription_coordinator")).CreateDelegate(typeof (Librdkafka._ConsumerGroupDescription_coordinator_delegate));
    Librdkafka._ConsumerGroupDescription_member_count = (Librdkafka._ConsumerGroupDescription_member_count_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConsumerGroupDescription_member_count")).CreateDelegate(typeof (Librdkafka._ConsumerGroupDescription_member_count_delegate));
    Librdkafka._ConsumerGroupDescription_authorized_operations = (Librdkafka._ConsumerGroupDescription_authorized_operations_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConsumerGroupDescription_authorized_operations")).CreateDelegate(typeof (Librdkafka._ConsumerGroupDescription_authorized_operations_delegate));
    Librdkafka._ConsumerGroupDescription_member = (Librdkafka._ConsumerGroupDescription_member_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ConsumerGroupDescription_member")).CreateDelegate(typeof (Librdkafka._ConsumerGroupDescription_member_delegate));
    Librdkafka._MemberDescription_client_id = (Librdkafka._MemberDescription_client_id_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_MemberDescription_client_id")).CreateDelegate(typeof (Librdkafka._MemberDescription_client_id_delegate));
    Librdkafka._MemberDescription_group_instance_id = (Librdkafka._MemberDescription_group_instance_id_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_MemberDescription_group_instance_id")).CreateDelegate(typeof (Librdkafka._MemberDescription_group_instance_id_delegate));
    Librdkafka._MemberDescription_consumer_id = (Librdkafka._MemberDescription_consumer_id_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_MemberDescription_consumer_id")).CreateDelegate(typeof (Librdkafka._MemberDescription_consumer_id_delegate));
    Librdkafka._MemberDescription_host = (Librdkafka._MemberDescription_host_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_MemberDescription_host")).CreateDelegate(typeof (Librdkafka._MemberDescription_host_delegate));
    Librdkafka._MemberDescription_assignment = (Librdkafka._MemberDescription_assignment_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_MemberDescription_assignment")).CreateDelegate(typeof (Librdkafka._MemberDescription_assignment_delegate));
    Librdkafka._MemberAssignment_partitions = (Librdkafka._MemberAssignment_partitions_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_MemberAssignment_partitions")).CreateDelegate(typeof (Librdkafka._MemberAssignment_partitions_delegate));
    Librdkafka._Node_id = (Librdkafka._Node_id_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_Node_id")).CreateDelegate(typeof (Librdkafka._Node_id_delegate));
    Librdkafka._Node_host = (Librdkafka._Node_host_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_Node_host")).CreateDelegate(typeof (Librdkafka._Node_host_delegate));
    Librdkafka._Node_port = (Librdkafka._Node_port_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_Node_port")).CreateDelegate(typeof (Librdkafka._Node_port_delegate));
    Librdkafka._Node_rack = (Librdkafka._Node_rack_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_Node_rack")).CreateDelegate(typeof (Librdkafka._Node_rack_delegate));
    Librdkafka._DescribeUserScramCredentials = (Librdkafka._DescribeUserScramCredentials_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeUserScramCredentials")).CreateDelegate(typeof (Librdkafka._DescribeUserScramCredentials_delegate));
    Librdkafka._DescribeUserScramCredentials_result_descriptions = (Librdkafka._DescribeUserScramCredentials_result_descriptions_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeUserScramCredentials_result_descriptions")).CreateDelegate(typeof (Librdkafka._DescribeUserScramCredentials_result_descriptions_delegate));
    Librdkafka._UserScramCredentialsDescription_user = (Librdkafka._UserScramCredentialsDescription_user_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_UserScramCredentialsDescription_user")).CreateDelegate(typeof (Librdkafka._UserScramCredentialsDescription_user_delegate));
    Librdkafka._UserScramCredentialsDescription_error = (Librdkafka._UserScramCredentialsDescription_error_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_UserScramCredentialsDescription_error")).CreateDelegate(typeof (Librdkafka._UserScramCredentialsDescription_error_delegate));
    Librdkafka._UserScramCredentialsDescription_scramcredentialinfo_count = (Librdkafka._UserScramCredentialsDescription_scramcredentialinfo_count_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_UserScramCredentialsDescription_scramcredentialinfo_count")).CreateDelegate(typeof (Librdkafka._UserScramCredentialsDescription_scramcredentialinfo_count_delegate));
    Librdkafka._UserScramCredentialsDescription_scramcredentialinfo = (Librdkafka._UserScramCredentialsDescription_scramcredentialinfo_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_UserScramCredentialsDescription_scramcredentialinfo")).CreateDelegate(typeof (Librdkafka._UserScramCredentialsDescription_scramcredentialinfo_delegate));
    Librdkafka._ScramCredentialInfo_mechanism = (Librdkafka._ScramCredentialInfo_mechanism_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ScramCredentialInfo_mechanism")).CreateDelegate(typeof (Librdkafka._ScramCredentialInfo_mechanism_delegate));
    Librdkafka._ScramCredentialInfo_iterations = (Librdkafka._ScramCredentialInfo_iterations_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ScramCredentialInfo_iterations")).CreateDelegate(typeof (Librdkafka._ScramCredentialInfo_iterations_delegate));
    Librdkafka._UserScramCredentialUpsertion_new = (Librdkafka._UserScramCredentialUpsertion_new_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_UserScramCredentialUpsertion_new")).CreateDelegate(typeof (Librdkafka._UserScramCredentialUpsertion_new_delegate));
    Librdkafka._UserScramCredentialDeletion_new = (Librdkafka._UserScramCredentialDeletion_new_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_UserScramCredentialDeletion_new")).CreateDelegate(typeof (Librdkafka._UserScramCredentialDeletion_new_delegate));
    Librdkafka._UserScramCredentialAlteration_destroy = (Librdkafka._UserScramCredentialAlteration_destroy_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_UserScramCredentialAlteration_destroy")).CreateDelegate(typeof (Librdkafka._UserScramCredentialAlteration_destroy_delegate));
    Librdkafka._AlterUserScramCredentials = (Librdkafka._AlterUserScramCredentials_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AlterUserScramCredentials")).CreateDelegate(typeof (Librdkafka._AlterUserScramCredentials_delegate));
    Librdkafka._AlterUserScramCredentials_result_responses = (Librdkafka._AlterUserScramCredentials_result_responses_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AlterUserScramCredentials_result_responses")).CreateDelegate(typeof (Librdkafka._AlterUserScramCredentials_result_responses_delegate));
    Librdkafka._AlterUserScramCredentials_result_response_user = (Librdkafka._AlterUserScramCredentials_result_response_user_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AlterUserScramCredentials_result_response_user")).CreateDelegate(typeof (Librdkafka._AlterUserScramCredentials_result_response_user_delegate));
    Librdkafka._AlterUserScramCredentials_result_response_error = (Librdkafka._AlterUserScramCredentials_result_response_error_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_AlterUserScramCredentials_result_response_error")).CreateDelegate(typeof (Librdkafka._AlterUserScramCredentials_result_response_error_delegate));
    Librdkafka._ListOffsets = (Librdkafka._ListOffsets_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ListOffsets")).CreateDelegate(typeof (Librdkafka._ListOffsets_delegate));
    Librdkafka._ListOffsets_result_infos = (Librdkafka._ListOffsets_result_infos_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ListOffsets_result_infos")).CreateDelegate(typeof (Librdkafka._ListOffsets_result_infos_delegate));
    Librdkafka._ListOffsetsResultInfo_timestamp = (Librdkafka._ListOffsetsResultInfo_timestamp_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ListOffsetsResultInfo_timestamp")).CreateDelegate(typeof (Librdkafka._ListOffsetsResultInfo_timestamp_delegate));
    Librdkafka._ListOffsetsResultInfo_topic_partition = (Librdkafka._ListOffsetsResultInfo_topic_partition_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_ListOffsetsResultInfo_topic_partition")).CreateDelegate(typeof (Librdkafka._ListOffsetsResultInfo_topic_partition_delegate));
    Librdkafka._DescribeTopics = (Librdkafka._DescribeTopics_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeTopics")).CreateDelegate(typeof (Librdkafka._DescribeTopics_delegate));
    Librdkafka._DescribeTopics_result_topics = (Librdkafka._DescribeTopics_result_topics_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeTopics_result_topics")).CreateDelegate(typeof (Librdkafka._DescribeTopics_result_topics_delegate));
    Librdkafka._TopicCollection_of_topic_names = (Librdkafka._TopicCollection_of_topic_names_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_TopicCollection_of_topic_names")).CreateDelegate(typeof (Librdkafka._TopicCollection_of_topic_names_delegate));
    Librdkafka._TopicCollection_destroy = (Librdkafka._TopicCollection_destroy_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_TopicCollection_destroy")).CreateDelegate(typeof (Librdkafka._TopicCollection_destroy_delegate));
    Librdkafka._TopicDescription_error = (Librdkafka._TopicDescription_error_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_TopicDescription_error")).CreateDelegate(typeof (Librdkafka._TopicDescription_error_delegate));
    Librdkafka._TopicDescription_name = (Librdkafka._TopicDescription_name_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_TopicDescription_name")).CreateDelegate(typeof (Librdkafka._TopicDescription_name_delegate));
    Librdkafka._TopicDescription_topic_id = (Librdkafka._TopicDescription_topic_id_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_TopicDescription_topic_id")).CreateDelegate(typeof (Librdkafka._TopicDescription_topic_id_delegate));
    Librdkafka._TopicDescription_partitions = (Librdkafka._TopicDescription_partitions_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_TopicDescription_partitions")).CreateDelegate(typeof (Librdkafka._TopicDescription_partitions_delegate));
    Librdkafka._TopicDescription_is_internal = (Librdkafka._TopicDescription_is_internal_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_TopicDescription_is_internal")).CreateDelegate(typeof (Librdkafka._TopicDescription_is_internal_delegate));
    Librdkafka._TopicDescription_authorized_operations = (Librdkafka._TopicDescription_authorized_operations_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_TopicDescription_authorized_operations")).CreateDelegate(typeof (Librdkafka._TopicDescription_authorized_operations_delegate));
    Librdkafka._TopicPartitionInfo_isr = (Librdkafka._TopicPartitionInfo_isr_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_TopicPartitionInfo_isr")).CreateDelegate(typeof (Librdkafka._TopicPartitionInfo_isr_delegate));
    Librdkafka._TopicPartitionInfo_leader = (Librdkafka._TopicPartitionInfo_leader_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_TopicPartitionInfo_leader")).CreateDelegate(typeof (Librdkafka._TopicPartitionInfo_leader_delegate));
    Librdkafka._TopicPartitionInfo_partition = (Librdkafka._TopicPartitionInfo_partition_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_TopicPartitionInfo_partition")).CreateDelegate(typeof (Librdkafka._TopicPartitionInfo_partition_delegate));
    Librdkafka._TopicPartitionInfo_replicas = (Librdkafka._TopicPartitionInfo_replicas_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_TopicPartitionInfo_replicas")).CreateDelegate(typeof (Librdkafka._TopicPartitionInfo_replicas_delegate));
    Librdkafka._DescribeCluster = (Librdkafka._DescribeCluster_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeCluster")).CreateDelegate(typeof (Librdkafka._DescribeCluster_delegate));
    Librdkafka._DescribeCluster_result_nodes = (Librdkafka._DescribeCluster_result_nodes_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeCluster_result_nodes")).CreateDelegate(typeof (Librdkafka._DescribeCluster_result_nodes_delegate));
    Librdkafka._DescribeCluster_result_authorized_operations = (Librdkafka._DescribeCluster_result_authorized_operations_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeCluster_result_authorized_operations")).CreateDelegate(typeof (Librdkafka._DescribeCluster_result_authorized_operations_delegate));
    Librdkafka._DescribeCluster_result_controller = (Librdkafka._DescribeCluster_result_controller_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeCluster_result_controller")).CreateDelegate(typeof (Librdkafka._DescribeCluster_result_controller_delegate));
    Librdkafka._DescribeCluster_result_cluster_id = (Librdkafka._DescribeCluster_result_cluster_id_delegate) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_DescribeCluster_result_cluster_id")).CreateDelegate(typeof (Librdkafka._DescribeCluster_result_cluster_id_delegate));
    Librdkafka._topic_result_error = (Func<IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_result_error")).CreateDelegate(typeof (Func<IntPtr, ErrorCode>));
    Librdkafka._topic_result_error_string = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_result_error_string")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._topic_result_name = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_topic_result_name")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._group_result_name = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_group_result_name")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._group_result_error = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_group_result_error")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._group_result_partitions = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_group_result_partitions")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    Librdkafka._destroy_flags = (Action<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_destroy_flags")).CreateDelegate(typeof (Action<IntPtr, IntPtr>));
    Librdkafka._error_code = (Func<IntPtr, ErrorCode>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_error_code")).CreateDelegate(typeof (Func<IntPtr, ErrorCode>));
    Librdkafka._error_string = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_error_string")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._error_is_fatal = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_error_is_fatal")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._error_is_retriable = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_error_is_retriable")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._error_txn_requires_abort = (Func<IntPtr, IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_error_txn_requires_abort")).CreateDelegate(typeof (Func<IntPtr, IntPtr>));
    Librdkafka._error_destroy = (Action<IntPtr>) ((IEnumerable<MethodInfo>) array).Single<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "rd_kafka_error_destroy")).CreateDelegate(typeof (Action<IntPtr>));
    try
    {
      IntPtr num = Librdkafka._err2str(ErrorCode.NoError);
    }
    catch (Exception ex)
    {
      return false;
    }
    return true;
  }

  public static bool IsInitialized
  {
    get
    {
      lock (Librdkafka.loadLockObj)
        return Librdkafka.isInitialized;
    }
  }

  public static bool Initialize(string userSpecifiedPath)
  {
    lock (Librdkafka.loadLockObj)
    {
      if (Librdkafka.isInitialized)
        return false;
      if (!MonoSupport.IsMonoRuntime)
        Librdkafka.LoadNetFrameworkDelegates(userSpecifiedPath);
      else if (Environment.OSVersion.Platform == PlatformID.Unix)
        Librdkafka.LoadLinuxDelegates(userSpecifiedPath);
      else if (Environment.OSVersion.Platform == PlatformID.MacOSX)
        Librdkafka.LoadOSXDelegates(userSpecifiedPath);
      else
        Librdkafka.LoadNetFrameworkDelegates(userSpecifiedPath);
      if ((long) Librdkafka.version() < 17105663L)
        throw new FileLoadException($"Invalid librdkafka version {(long) Librdkafka.version():x}, expected at least {17105663L:x}");
      Librdkafka.isInitialized = true;
      return true;
    }
  }

  private static void LoadNetFrameworkDelegates(string userSpecifiedPath)
  {
    string str = userSpecifiedPath;
    if (str == null)
    {
      bool flag = IntPtr.Size == 8;
      string directoryName = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().EscapedCodeBase).LocalPath);
      str = Path.Combine(Path.Combine(directoryName, flag ? Path.Combine("librdkafka", "x64") : Path.Combine("librdkafka", "x86")), "librdkafka.dll");
      if (!File.Exists(str))
        str = Path.Combine(Path.Combine(directoryName, flag ? "runtimes\\win-x64\\native" : "runtimes\\win-x86\\native"), "librdkafka.dll");
      if (!File.Exists(str))
        str = Path.Combine(Path.Combine(directoryName, flag ? "x64" : "x86"), "librdkafka.dll");
      if (!File.Exists(str))
        str = Path.Combine(directoryName, "librdkafka.dll");
    }
    if (Librdkafka.WindowsNative.LoadLibraryEx(str, IntPtr.Zero, Librdkafka.WindowsNative.LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH) == IntPtr.Zero)
    {
      Win32Exception innerException = new Win32Exception();
      throw new InvalidOperationException($"Error while loading librdkafka.dll or its dependencies from {str}. Check the directory exists, if not check your deployment process. You can also load the library and its dependencies by yourself before any call to Confluent.Kafka", (Exception) innerException);
    }
    if (!Librdkafka.SetDelegates(typeof (Confluent.Kafka.Impl.NativeMethods.NativeMethods)))
      throw new DllNotFoundException("Failed to load the librdkafka native library.");
  }

  private static bool TrySetDelegates(List<Type> nativeMethodCandidateTypes)
  {
    foreach (Type methodCandidateType in nativeMethodCandidateTypes)
    {
      if (Librdkafka.SetDelegates(methodCandidateType))
        return true;
    }
    throw new DllNotFoundException("Failed to load the librdkafka native library.");
  }

  private static void LoadNetStandardDelegates(string userSpecifiedPath)
  {
    if (userSpecifiedPath != null && Librdkafka.WindowsNative.LoadLibraryEx(userSpecifiedPath, IntPtr.Zero, Librdkafka.WindowsNative.LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH) == IntPtr.Zero)
      throw new InvalidOperationException($"Failed to load librdkafka at location '{userSpecifiedPath}'. Win32 error: {System.Runtime.InteropServices.Marshal.GetLastWin32Error()}");
    Librdkafka.TrySetDelegates(new List<Type>()
    {
      typeof (Confluent.Kafka.Impl.NativeMethods.NativeMethods)
    });
  }

  private static void LoadOSXDelegates(string userSpecifiedPath)
  {
    if (userSpecifiedPath != null && Librdkafka.PosixNative.dlopen(userSpecifiedPath, 2) == IntPtr.Zero)
      throw new InvalidOperationException($"Failed to load librdkafka at location '{userSpecifiedPath}'. dlerror: '{Librdkafka.PosixNative.LastError}'.");
    Librdkafka.TrySetDelegates(new List<Type>()
    {
      typeof (Confluent.Kafka.Impl.NativeMethods.NativeMethods)
    });
  }

  private static void LoadLinuxDelegates(string userSpecifiedPath)
  {
    if (userSpecifiedPath != null)
    {
      if (Librdkafka.PosixNative.dlopen(userSpecifiedPath, 2) == IntPtr.Zero)
        throw new InvalidOperationException($"Failed to load librdkafka at location '{userSpecifiedPath}'. dlerror: '{Librdkafka.PosixNative.LastError}'.");
      Librdkafka.TrySetDelegates(new List<Type>()
      {
        typeof (Confluent.Kafka.Impl.NativeMethods.NativeMethods)
      });
    }
    else
    {
      List<Type> nativeMethodCandidateTypes = new List<Type>();
      if (PlatformApis.GetOSName().Equals("alpine", StringComparison.OrdinalIgnoreCase))
      {
        nativeMethodCandidateTypes.Add(typeof (NativeMethods_Alpine));
      }
      else
      {
        nativeMethodCandidateTypes.Add(typeof (NativeMethods_Centos7));
        nativeMethodCandidateTypes.Add(typeof (Confluent.Kafka.Impl.NativeMethods.NativeMethods));
        nativeMethodCandidateTypes.Add(typeof (NativeMethods_Centos6));
      }
      Librdkafka.TrySetDelegates(nativeMethodCandidateTypes);
    }
  }

  internal static IntPtr version() => Librdkafka._version();

  internal static IntPtr version_str() => Librdkafka._version_str();

  internal static IntPtr get_debug_contexts() => Librdkafka._get_debug_contexts();

  internal static IntPtr err2str(ErrorCode err) => Librdkafka._err2str(err);

  internal static IntPtr topic_partition_list_new(IntPtr size)
  {
    return Librdkafka._topic_partition_list_new(size);
  }

  internal static void topic_partition_list_destroy(IntPtr rkparlist)
  {
    Librdkafka._topic_partition_list_destroy(rkparlist);
  }

  internal static IntPtr topic_partition_list_add(IntPtr rktparlist, string topic, int partition)
  {
    return Librdkafka._topic_partition_list_add(rktparlist, topic, partition);
  }

  internal static IntPtr headers_new(IntPtr size) => Librdkafka._headers_new(size);

  internal static void headers_destroy(IntPtr hdrs) => Librdkafka._headers_destroy(hdrs);

  internal static ErrorCode headers_add(
    IntPtr hdrs,
    IntPtr keydata,
    IntPtr keylen,
    IntPtr valdata,
    IntPtr vallen)
  {
    return Librdkafka._header_add(hdrs, keydata, keylen, valdata, vallen);
  }

  internal static ErrorCode header_get_all(
    IntPtr hdrs,
    IntPtr idx,
    out IntPtr namep,
    out IntPtr valuep,
    out IntPtr sizep)
  {
    return Librdkafka._header_get_all(hdrs, idx, out namep, out valuep, out sizep);
  }

  internal static ErrorCode last_error() => Librdkafka._last_error();

  internal static ErrorCode fatal_error(IntPtr rk, StringBuilder sb, UIntPtr len)
  {
    return Librdkafka._fatal_error(rk, sb, len);
  }

  internal static IntPtr message_errstr(IntPtr rkmessage) => Librdkafka._message_errstr(rkmessage);

  internal static long message_timestamp(IntPtr rkmessage, out IntPtr tstype)
  {
    return Librdkafka._message_timestamp(rkmessage, out tstype);
  }

  internal static PersistenceStatus message_status(IntPtr rkmessage)
  {
    return Librdkafka._message_status(rkmessage);
  }

  internal static ErrorCode message_headers(IntPtr rkmessage, out IntPtr hdrs)
  {
    return Librdkafka._message_headers(rkmessage, out hdrs);
  }

  internal static int message_leader_epoch(IntPtr rkmessage)
  {
    return Librdkafka._message_leader_epoch(rkmessage);
  }

  internal static void message_destroy(IntPtr rkmessage) => Librdkafka._message_destroy(rkmessage);

  internal static SafeConfigHandle conf_new() => Librdkafka._conf_new();

  internal static void conf_destroy(IntPtr conf) => Librdkafka._conf_destroy(conf);

  internal static IntPtr conf_dup(IntPtr conf) => Librdkafka._conf_dup(conf);

  internal static ConfRes conf_set(
    IntPtr conf,
    string name,
    string value,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._conf_set(conf, name, value, errstr, errstr_size);
  }

  internal static void conf_set_dr_msg_cb(IntPtr conf, Librdkafka.DeliveryReportDelegate dr_msg_cb)
  {
    Librdkafka._conf_set_dr_msg_cb(conf, dr_msg_cb);
  }

  internal static void conf_set_rebalance_cb(IntPtr conf, Librdkafka.RebalanceDelegate rebalance_cb)
  {
    Librdkafka._conf_set_rebalance_cb(conf, rebalance_cb);
  }

  internal static void conf_set_offset_commit_cb(IntPtr conf, Librdkafka.CommitDelegate commit_cb)
  {
    Librdkafka._conf_set_offset_commit_cb(conf, commit_cb);
  }

  internal static void conf_set_error_cb(IntPtr conf, Librdkafka.ErrorDelegate error_cb)
  {
    Librdkafka._conf_set_error_cb(conf, error_cb);
  }

  internal static void conf_set_log_cb(IntPtr conf, Librdkafka.LogDelegate log_cb)
  {
    Librdkafka._conf_set_log_cb(conf, log_cb);
  }

  internal static void conf_set_stats_cb(IntPtr conf, Librdkafka.StatsDelegate stats_cb)
  {
    Librdkafka._conf_set_stats_cb(conf, stats_cb);
  }

  internal static void conf_set_oauthbearer_token_refresh_cb(
    IntPtr conf,
    Librdkafka.OAuthBearerTokenRefreshDelegate oauthbearer_token_refresh_cb)
  {
    Librdkafka._conf_set_oauthbearer_token_refresh_cb(conf, oauthbearer_token_refresh_cb);
  }

  internal static ErrorCode oauthbearer_set_token(
    IntPtr rk,
    string token_value,
    long md_lifetime_ms,
    string md_principal_name,
    string[] extensions,
    UIntPtr extensions_size,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._oauthbearer_set_token(rk, token_value, md_lifetime_ms, md_principal_name, extensions, extensions_size, errstr, errstr_size);
  }

  internal static ErrorCode oauthbearer_set_token_failure(IntPtr rk, string errstr)
  {
    return Librdkafka._oauthbearer_set_token_failure(rk, errstr);
  }

  internal static void conf_set_default_topic_conf(IntPtr conf, IntPtr tconf)
  {
    Librdkafka._conf_set_default_topic_conf(conf, tconf);
  }

  internal static SafeTopicConfigHandle conf_get_default_topic_conf(SafeConfigHandle conf)
  {
    return Librdkafka._conf_get_default_topic_conf(conf);
  }

  internal static ConfRes conf_get(
    IntPtr conf,
    string name,
    StringBuilder dest,
    ref UIntPtr dest_size)
  {
    return Librdkafka._conf_get(conf, name, dest, ref dest_size);
  }

  internal static ConfRes topic_conf_get(
    IntPtr conf,
    string name,
    StringBuilder dest,
    ref UIntPtr dest_size)
  {
    return Librdkafka._topic_conf_get(conf, name, dest, ref dest_size);
  }

  internal static IntPtr conf_dump(IntPtr conf, out UIntPtr cntp)
  {
    return Librdkafka._conf_dump(conf, out cntp);
  }

  internal static IntPtr topic_conf_dump(IntPtr conf, out UIntPtr cntp)
  {
    return Librdkafka._topic_conf_dump(conf, out cntp);
  }

  internal static void conf_dump_free(IntPtr arr, UIntPtr cnt)
  {
    Librdkafka._conf_dump_free(arr, cnt);
  }

  internal static SafeTopicConfigHandle topic_conf_new() => Librdkafka._topic_conf_new();

  internal static SafeTopicConfigHandle topic_conf_dup(SafeTopicConfigHandle conf)
  {
    return Librdkafka._topic_conf_dup(conf);
  }

  internal static SafeTopicConfigHandle default_topic_conf_dup(SafeKafkaHandle rk)
  {
    return Librdkafka._default_topic_conf_dup(rk);
  }

  internal static void topic_conf_destroy(IntPtr conf) => Librdkafka._topic_conf_destroy(conf);

  internal static ConfRes topic_conf_set(
    IntPtr conf,
    string name,
    string value,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._topic_conf_set(conf, name, value, errstr, errstr_size);
  }

  internal static void topic_conf_set_opaque(IntPtr topic_conf, IntPtr opaque)
  {
    Librdkafka._topic_conf_set_opaque(topic_conf, opaque);
  }

  internal static void topic_conf_set_partitioner_cb(
    IntPtr topic_conf,
    Librdkafka.PartitionerDelegate partitioner_cb)
  {
    Librdkafka._topic_conf_set_partitioner_cb(topic_conf, partitioner_cb);
  }

  internal static bool topic_partition_available(IntPtr rkt, int partition)
  {
    return Librdkafka._topic_partition_available(rkt, partition);
  }

  internal static int topic_partition_get_leader_epoch(IntPtr rkt)
  {
    return Librdkafka._topic_partition_get_leader_epoch(rkt);
  }

  internal static void topic_partition_set_leader_epoch(IntPtr rkt, int leader_epoch)
  {
    Librdkafka._topic_partition_set_leader_epoch(rkt, leader_epoch);
  }

  internal static IntPtr init_transactions(IntPtr rk, IntPtr timeout)
  {
    return Librdkafka._init_transactions(rk, timeout);
  }

  internal static IntPtr begin_transaction(IntPtr rk) => Librdkafka._begin_transaction(rk);

  internal static IntPtr commit_transaction(IntPtr rk, IntPtr timeout)
  {
    return Librdkafka._commit_transaction(rk, timeout);
  }

  internal static IntPtr abort_transaction(IntPtr rk, IntPtr timeout)
  {
    return Librdkafka._abort_transaction(rk, timeout);
  }

  internal static IntPtr send_offsets_to_transaction(
    IntPtr rk,
    IntPtr offsets,
    IntPtr consumer_group_metadata,
    IntPtr timeout_ms)
  {
    return Librdkafka._send_offsets_to_transaction(rk, offsets, consumer_group_metadata, timeout_ms);
  }

  internal static IntPtr consumer_group_metadata(IntPtr rk)
  {
    return Librdkafka._rd_kafka_consumer_group_metadata(rk);
  }

  internal static void consumer_group_metadata_destroy(IntPtr rk)
  {
    Librdkafka._rd_kafka_consumer_group_metadata_destroy(rk);
  }

  internal static IntPtr consumer_group_metadata_write(
    IntPtr cgmd,
    out IntPtr data,
    out IntPtr dataSize)
  {
    return Librdkafka._rd_kafka_consumer_group_metadata_write(cgmd, out data, out dataSize);
  }

  internal static IntPtr consumer_group_metadata_read(
    out IntPtr cgmd,
    byte[] data,
    IntPtr dataSize)
  {
    return Librdkafka._rd_kafka_consumer_group_metadata_read(out cgmd, data, dataSize);
  }

  internal static SafeKafkaHandle kafka_new(
    RdKafkaType type,
    IntPtr conf,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._new(type, conf, errstr, errstr_size);
  }

  internal static void destroy(IntPtr rk) => Librdkafka._destroy(rk);

  internal static void destroy_flags(IntPtr rk, IntPtr flags)
  {
    Librdkafka._destroy_flags(rk, flags);
  }

  internal static IntPtr name(IntPtr rk) => Librdkafka._name(rk);

  internal static IntPtr memberid(IntPtr rk) => Librdkafka._memberid(rk);

  internal static IntPtr Uuid_new(long most_significant_bits, long least_significant_bits)
  {
    return Librdkafka._Uuid_new(most_significant_bits, least_significant_bits);
  }

  internal static IntPtr Uuid_base64str(IntPtr uuid) => Librdkafka._Uuid_base64str(uuid);

  internal static long Uuid_most_significant_bits(IntPtr uuid)
  {
    return Librdkafka._Uuid_most_significant_bits(uuid);
  }

  internal static long Uuid_least_significant_bits(IntPtr uuid)
  {
    return Librdkafka._Uuid_least_significant_bits(uuid);
  }

  internal static void Uuid_destroy(IntPtr uuid) => Librdkafka._Uuid_destroy(uuid);

  internal static SafeTopicHandle topic_new(IntPtr rk, IntPtr topic, IntPtr conf)
  {
    return Librdkafka._topic_new(rk, topic, conf);
  }

  internal static void topic_destroy(IntPtr rk) => Librdkafka._topic_destroy(rk);

  internal static IntPtr topic_name(IntPtr rkt) => Librdkafka._topic_name(rkt);

  internal static ErrorCode poll_set_consumer(IntPtr rk) => Librdkafka._poll_set_consumer(rk);

  internal static IntPtr poll(IntPtr rk, IntPtr timeout_ms) => Librdkafka._poll(rk, timeout_ms);

  internal static ErrorCode query_watermark_offsets(
    IntPtr rk,
    string topic,
    int partition,
    out long low,
    out long high,
    IntPtr timeout_ms)
  {
    return Librdkafka._query_watermark_offsets(rk, topic, partition, out low, out high, timeout_ms);
  }

  internal static ErrorCode get_watermark_offsets(
    IntPtr rk,
    string topic,
    int partition,
    out long low,
    out long high)
  {
    return Librdkafka._get_watermark_offsets(rk, topic, partition, out low, out high);
  }

  internal static ErrorCode offsets_for_times(IntPtr rk, IntPtr offsets, IntPtr timeout_ms)
  {
    return Librdkafka._offsets_for_times(rk, offsets, timeout_ms);
  }

  internal static void mem_free(IntPtr rk, IntPtr ptr) => Librdkafka._mem_free(rk, ptr);

  internal static ErrorCode subscribe(IntPtr rk, IntPtr topics)
  {
    return Librdkafka._subscribe(rk, topics);
  }

  internal static ErrorCode unsubscribe(IntPtr rk) => Librdkafka._unsubscribe(rk);

  internal static ErrorCode subscription(IntPtr rk, out IntPtr topics)
  {
    return Librdkafka._subscription(rk, out topics);
  }

  internal static IntPtr consumer_poll(IntPtr rk, IntPtr timeout_ms)
  {
    return Librdkafka._consumer_poll(rk, timeout_ms);
  }

  internal static ErrorCode consumer_close(IntPtr rk) => Librdkafka._consumer_close(rk);

  internal static ErrorCode assign(IntPtr rk, IntPtr partitions)
  {
    return Librdkafka._assign(rk, partitions);
  }

  internal static IntPtr incremental_assign(IntPtr rk, IntPtr partitions)
  {
    return Librdkafka._incremental_assign(rk, partitions);
  }

  internal static IntPtr incremental_unassign(IntPtr rk, IntPtr partitions)
  {
    return Librdkafka._incremental_unassign(rk, partitions);
  }

  internal static IntPtr assignment_lost(IntPtr rk) => Librdkafka._assignment_lost(rk);

  internal static IntPtr rebalance_protocol(IntPtr rk) => Librdkafka._rebalance_protocol(rk);

  internal static ErrorCode assignment(IntPtr rk, out IntPtr topics)
  {
    return Librdkafka._assignment(rk, out topics);
  }

  internal static ErrorCode offsets_store(IntPtr rk, IntPtr offsets)
  {
    return Librdkafka._offsets_store(rk, offsets);
  }

  internal static ErrorCode commit(IntPtr rk, IntPtr offsets, bool async)
  {
    return Librdkafka._commit(rk, offsets, async);
  }

  internal static ErrorCode commit_queue(
    IntPtr rk,
    IntPtr offsets,
    IntPtr rkqu,
    Librdkafka.CommitDelegate cb,
    IntPtr opaque)
  {
    return Librdkafka._commit_queue(rk, offsets, rkqu, cb, opaque);
  }

  internal static ErrorCode pause_partitions(IntPtr rk, IntPtr partitions)
  {
    return Librdkafka._pause_partitions(rk, partitions);
  }

  internal static ErrorCode resume_partitions(IntPtr rk, IntPtr partitions)
  {
    return Librdkafka._resume_partitions(rk, partitions);
  }

  internal static ErrorCode seek(IntPtr rkt, int partition, long offset, IntPtr timeout_ms)
  {
    return Librdkafka._seek(rkt, partition, offset, timeout_ms);
  }

  internal static IntPtr seek_partitions(IntPtr rkt, IntPtr partitions, IntPtr timeout_ms)
  {
    return Librdkafka._seek_partitions(rkt, partitions, timeout_ms);
  }

  internal static ErrorCode committed(IntPtr rk, IntPtr partitions, IntPtr timeout_ms)
  {
    return Librdkafka._committed(rk, partitions, timeout_ms);
  }

  internal static ErrorCode position(IntPtr rk, IntPtr partitions)
  {
    return Librdkafka._position(rk, partitions);
  }

  internal static unsafe ErrorCode produceva(
    IntPtr rk,
    string topic,
    int partition,
    IntPtr msgflags,
    IntPtr val,
    UIntPtr len,
    IntPtr key,
    UIntPtr keylen,
    long timestamp,
    IntPtr headers,
    IntPtr msg_opaque)
  {
    IntPtr hglobalAnsi = System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(topic);
    try
    {
      rd_kafka_vu* rdKafkaVuPtr = stackalloc rd_kafka_vu[8];
      rdKafkaVuPtr[0] = new rd_kafka_vu()
      {
        vt = rd_kafka_vtype.Topic,
        data = new vu_data() { topic = hglobalAnsi }
      };
      *(rdKafkaVuPtr + 1) = new rd_kafka_vu()
      {
        vt = rd_kafka_vtype.Partition,
        data = new vu_data() { partition = partition }
      };
      *(rdKafkaVuPtr + 2) = new rd_kafka_vu()
      {
        vt = rd_kafka_vtype.MsgFlags,
        data = new vu_data() { msgflags = msgflags }
      };
      *(rdKafkaVuPtr + 3) = new rd_kafka_vu()
      {
        vt = rd_kafka_vtype.Value,
        data = new vu_data()
        {
          val = new ptr_and_size() { ptr = val, size = len }
        }
      };
      *(rdKafkaVuPtr + 4) = new rd_kafka_vu()
      {
        vt = rd_kafka_vtype.Key,
        data = new vu_data()
        {
          key = new ptr_and_size()
          {
            ptr = key,
            size = keylen
          }
        }
      };
      *(rdKafkaVuPtr + 5) = new rd_kafka_vu()
      {
        vt = rd_kafka_vtype.Timestamp,
        data = new vu_data() { timestamp = timestamp }
      };
      *(rdKafkaVuPtr + 6) = new rd_kafka_vu()
      {
        vt = rd_kafka_vtype.Headers,
        data = new vu_data() { headers = headers }
      };
      *(rdKafkaVuPtr + 7) = new rd_kafka_vu()
      {
        vt = rd_kafka_vtype.Opaque,
        data = new vu_data() { opaque = msg_opaque }
      };
      rd_kafka_vu* vus = rdKafkaVuPtr;
      return Librdkafka.GetErrorCodeAndDestroy(Librdkafka._produceva(rk, vus, new IntPtr(8)));
    }
    finally
    {
      System.Runtime.InteropServices.Marshal.FreeHGlobal(hglobalAnsi);
    }
  }

  private static ErrorCode GetErrorCodeAndDestroy(IntPtr ptr)
  {
    if (ptr == IntPtr.Zero)
      return ErrorCode.NoError;
    int errorCodeAndDestroy = (int) Librdkafka.error_code(ptr);
    Librdkafka.error_destroy(ptr);
    return (ErrorCode) errorCodeAndDestroy;
  }

  internal static ErrorCode flush(IntPtr rk, IntPtr timeout_ms)
  {
    return Librdkafka._flush(rk, timeout_ms);
  }

  internal static ErrorCode metadata(
    IntPtr rk,
    bool all_topics,
    IntPtr only_rkt,
    out IntPtr metadatap,
    IntPtr timeout_ms)
  {
    return Librdkafka._metadata(rk, all_topics, only_rkt, out metadatap, timeout_ms);
  }

  internal static void metadata_destroy(IntPtr metadata) => Librdkafka._metadata_destroy(metadata);

  internal static ErrorCode list_groups(
    IntPtr rk,
    string group,
    out IntPtr grplistp,
    IntPtr timeout_ms)
  {
    return Librdkafka._list_groups(rk, group, out grplistp, timeout_ms);
  }

  internal static void group_list_destroy(IntPtr grplist)
  {
    Librdkafka._group_list_destroy(grplist);
  }

  internal static IntPtr brokers_add(IntPtr rk, string brokerlist)
  {
    return Librdkafka._brokers_add(rk, brokerlist);
  }

  internal static IntPtr sasl_set_credentials(IntPtr rk, string username, string password)
  {
    return Librdkafka._sasl_set_credentials(rk, username, password);
  }

  internal static int outq_len(IntPtr rk) => Librdkafka._outq_len(rk);

  internal static IntPtr AdminOptions_new(IntPtr rk, Librdkafka.AdminOp op)
  {
    return Librdkafka._AdminOptions_new(rk, op);
  }

  internal static void AdminOptions_destroy(IntPtr options)
  {
    Librdkafka._AdminOptions_destroy(options);
  }

  internal static ErrorCode AdminOptions_set_request_timeout(
    IntPtr options,
    IntPtr timeout_ms,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._AdminOptions_set_request_timeout(options, timeout_ms, errstr, errstr_size);
  }

  internal static ErrorCode AdminOptions_set_operation_timeout(
    IntPtr options,
    IntPtr timeout_ms,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._AdminOptions_set_operation_timeout(options, timeout_ms, errstr, errstr_size);
  }

  internal static ErrorCode AdminOptions_set_validate_only(
    IntPtr options,
    IntPtr true_or_false,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._AdminOptions_set_validate_only(options, true_or_false, errstr, errstr_size);
  }

  internal static ErrorCode AdminOptions_set_incremental(
    IntPtr options,
    IntPtr true_or_false,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._AdminOptions_set_incremental(options, true_or_false, errstr, errstr_size);
  }

  internal static ErrorCode AdminOptions_set_broker(
    IntPtr options,
    int broker_id,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._AdminOptions_set_broker(options, broker_id, errstr, errstr_size);
  }

  internal static void AdminOptions_set_opaque(IntPtr options, IntPtr opaque)
  {
    Librdkafka._AdminOptions_set_opaque(options, opaque);
  }

  internal static IntPtr AdminOptions_set_require_stable_offsets(
    IntPtr options,
    IntPtr true_or_false)
  {
    return Librdkafka._AdminOptions_set_require_stable_offsets(options, true_or_false);
  }

  internal static IntPtr AdminOptions_set_include_authorized_operations(
    IntPtr options,
    IntPtr true_or_false)
  {
    return Librdkafka._AdminOptions_set_include_authorized_operations(options, true_or_false);
  }

  internal static IntPtr AdminOptions_set_match_consumer_group_states(
    IntPtr options,
    ConsumerGroupState[] states,
    UIntPtr statesCnt)
  {
    return Librdkafka._AdminOptions_set_match_consumer_group_states(options, states, statesCnt);
  }

  internal static IntPtr AdminOptions_set_isolation_level(IntPtr options, IntPtr IsolationLevel)
  {
    return Librdkafka._AdminOptions_set_isolation_level(options, IsolationLevel);
  }

  internal static IntPtr NewTopic_new(
    string topic,
    IntPtr num_partitions,
    IntPtr replication_factor,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._NewTopic_new(topic, num_partitions, replication_factor, errstr, errstr_size);
  }

  internal static void NewTopic_destroy(IntPtr new_topic)
  {
    Librdkafka._NewTopic_destroy(new_topic);
  }

  internal static ErrorCode NewTopic_set_replica_assignment(
    IntPtr new_topic,
    int partition,
    int[] broker_ids,
    UIntPtr broker_id_cnt,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._NewTopic_set_replica_assignment(new_topic, partition, broker_ids, broker_id_cnt, errstr, errstr_size);
  }

  internal static ErrorCode NewTopic_set_config(IntPtr new_topic, string name, string value)
  {
    return Librdkafka._NewTopic_set_config(new_topic, name, value);
  }

  internal static void CreateTopics(
    IntPtr rk,
    IntPtr[] new_topics,
    UIntPtr new_topic_cnt,
    IntPtr options,
    IntPtr rkqu)
  {
    Librdkafka._CreateTopics(rk, new_topics, new_topic_cnt, options, rkqu);
  }

  internal static IntPtr CreateTopics_result_topics(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._CreateTopics_result_topics(result, out cntp);
  }

  internal static IntPtr DeleteTopic_new(string topic) => Librdkafka._DeleteTopic_new(topic);

  internal static void DeleteTopic_destroy(IntPtr del_topic)
  {
    Librdkafka._DeleteTopic_destroy(del_topic);
  }

  internal static void DeleteTopics(
    IntPtr rk,
    IntPtr[] del_topics,
    UIntPtr del_topic_cnt,
    IntPtr options,
    IntPtr rkqu)
  {
    Librdkafka._DeleteTopics(rk, del_topics, del_topic_cnt, options, rkqu);
  }

  internal static IntPtr DeleteTopics_result_topics(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._DeleteTopics_result_topics(result, out cntp);
  }

  internal static IntPtr DeleteGroup_new(string group) => Librdkafka._DeleteGroup_new(group);

  internal static void DeleteGroup_destroy(IntPtr del_group)
  {
    Librdkafka._DeleteGroup_destroy(del_group);
  }

  internal static void DeleteGroups(
    IntPtr rk,
    IntPtr[] del_groups,
    UIntPtr del_groups_cnt,
    IntPtr options,
    IntPtr rkqu)
  {
    Librdkafka._DeleteGroups(rk, del_groups, del_groups_cnt, options, rkqu);
  }

  internal static IntPtr DeleteGroups_result_groups(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._DeleteGroups_result_groups(result, out cntp);
  }

  internal static IntPtr NewPartitions_new(
    string topic,
    UIntPtr new_total_cnt,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._NewPartitions_new(topic, new_total_cnt, errstr, errstr_size);
  }

  internal static void NewPartitions_destroy(IntPtr new_parts)
  {
    Librdkafka._NewPartitions_destroy(new_parts);
  }

  internal static ErrorCode NewPartitions_set_replica_assignment(
    IntPtr new_parts,
    int new_partition_idx,
    int[] broker_ids,
    UIntPtr broker_id_cnt,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._NewPartitions_set_replica_assignment(new_parts, new_partition_idx, broker_ids, broker_id_cnt, errstr, errstr_size);
  }

  internal static void CreatePartitions(
    IntPtr rk,
    IntPtr[] new_parts,
    UIntPtr new_parts_cnt,
    IntPtr options,
    IntPtr rkqu)
  {
    Librdkafka._CreatePartitions(rk, new_parts, new_parts_cnt, options, rkqu);
  }

  internal static IntPtr CreatePartitions_result_topics(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._CreatePartitions_result_topics(result, out cntp);
  }

  internal static IntPtr ConfigSource_name(ConfigSource configsource)
  {
    return Librdkafka._ConfigSource_name(configsource);
  }

  internal static IntPtr ConfigEntry_name(IntPtr entry) => Librdkafka._ConfigEntry_name(entry);

  internal static IntPtr ConfigEntry_value(IntPtr entry) => Librdkafka._ConfigEntry_value(entry);

  internal static ConfigSource ConfigEntry_source(IntPtr entry)
  {
    return Librdkafka._ConfigEntry_source(entry);
  }

  internal static IntPtr ConfigEntry_is_read_only(IntPtr entry)
  {
    return Librdkafka._ConfigEntry_is_read_only(entry);
  }

  internal static IntPtr ConfigEntry_is_default(IntPtr entry)
  {
    return Librdkafka._ConfigEntry_is_default(entry);
  }

  internal static IntPtr ConfigEntry_is_sensitive(IntPtr entry)
  {
    return Librdkafka._ConfigEntry_is_sensitive(entry);
  }

  internal static IntPtr ConfigEntry_is_synonym(IntPtr entry)
  {
    return Librdkafka._ConfigEntry_is_synonym(entry);
  }

  internal static IntPtr ConfigEntry_synonyms(IntPtr entry, out UIntPtr cntp)
  {
    return Librdkafka._ConfigEntry_synonyms(entry, out cntp);
  }

  internal static IntPtr ResourceType_name(ResourceType restype)
  {
    return Librdkafka._ResourceType_name(restype);
  }

  internal static IntPtr ConfigResource_new(ResourceType restype, string resname)
  {
    return Librdkafka._ConfigResource_new(restype, resname);
  }

  internal static void ConfigResource_destroy(IntPtr config)
  {
    Librdkafka._ConfigResource_destroy(config);
  }

  internal static ErrorCode ConfigResource_add_config(IntPtr config, string name, string value)
  {
    return Librdkafka._ConfigResource_add_config(config, name, value);
  }

  internal static ErrorCode ConfigResource_set_config(IntPtr config, string name, string value)
  {
    return Librdkafka._ConfigResource_set_config(config, name, value);
  }

  internal static ErrorCode ConfigResource_delete_config(IntPtr config, string name)
  {
    return Librdkafka._ConfigResource_delete_config(config, name);
  }

  internal static IntPtr ConfigResource_add_incremental_config(
    IntPtr config,
    string name,
    AlterConfigOpType optype,
    string value)
  {
    return Librdkafka._ConfigResource_add_incremental_config(config, name, optype, value);
  }

  internal static IntPtr ConfigResource_configs(IntPtr config, out UIntPtr cntp)
  {
    return Librdkafka._ConfigResource_configs(config, out cntp);
  }

  internal static ResourceType ConfigResource_type(IntPtr config)
  {
    return Librdkafka._ConfigResource_type(config);
  }

  internal static IntPtr ConfigResource_name(IntPtr config)
  {
    return Librdkafka._ConfigResource_name(config);
  }

  internal static ErrorCode ConfigResource_error(IntPtr config)
  {
    return Librdkafka._ConfigResource_error(config);
  }

  internal static IntPtr ConfigResource_error_string(IntPtr config)
  {
    return Librdkafka._ConfigResource_error_string(config);
  }

  internal static void AlterConfigs(
    IntPtr rk,
    IntPtr[] configs,
    UIntPtr config_cnt,
    IntPtr options,
    IntPtr rkqu)
  {
    Librdkafka._AlterConfigs(rk, configs, config_cnt, options, rkqu);
  }

  internal static IntPtr AlterConfigs_result_resources(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._AlterConfigs_result_resources(result, out cntp);
  }

  internal static void IncrementalAlterConfigs(
    IntPtr rk,
    IntPtr[] configs,
    UIntPtr config_cnt,
    IntPtr options,
    IntPtr rkqu)
  {
    Librdkafka._IncrementalAlterConfigs(rk, configs, config_cnt, options, rkqu);
  }

  internal static IntPtr IncrementalAlterConfigs_result_resources(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._IncrementalAlterConfigs_result_resources(result, out cntp);
  }

  internal static void DescribeConfigs(
    IntPtr rk,
    IntPtr[] configs,
    UIntPtr config_cnt,
    IntPtr options,
    IntPtr rkqu)
  {
    Librdkafka._DescribeConfigs(rk, configs, config_cnt, options, rkqu);
  }

  internal static IntPtr DescribeConfigs_result_resources(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._DescribeConfigs_result_resources(result, out cntp);
  }

  internal static IntPtr DeleteRecords_new(IntPtr topicPartitionOffsets)
  {
    return Librdkafka._DeleteRecords_new(topicPartitionOffsets);
  }

  internal static void DeleteRecords_destroy(IntPtr del_records)
  {
    Librdkafka._DeleteRecords_destroy(del_records);
  }

  internal static void DeleteRecords(
    IntPtr rk,
    IntPtr[] del_records,
    UIntPtr del_records_cnt,
    IntPtr options,
    IntPtr rkqu)
  {
    Librdkafka._DeleteRecords(rk, del_records, del_records_cnt, options, rkqu);
  }

  internal static IntPtr DeleteRecords_result_offsets(IntPtr result)
  {
    return Librdkafka._DeleteRecords_result_offsets(result);
  }

  internal static IntPtr DeleteConsumerGroupOffsets_new(string group, IntPtr topicPartitionOffsets)
  {
    return Librdkafka._DeleteConsumerGroupOffsets_new(group, topicPartitionOffsets);
  }

  internal static void DeleteConsumerGroupOffsets_destroy(IntPtr del_grp_offsets)
  {
    Librdkafka._DeleteConsumerGroupOffsets_destroy(del_grp_offsets);
  }

  internal static void DeleteConsumerGroupOffsets(
    IntPtr rk,
    IntPtr[] del_grp_offsets,
    UIntPtr del_grp_offsets_cnt,
    IntPtr options,
    IntPtr rkqu)
  {
    Librdkafka._DeleteConsumerGroupOffsets(rk, del_grp_offsets, del_grp_offsets_cnt, options, rkqu);
  }

  internal static IntPtr DeleteConsumerGroupOffsets_result_groups(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._DeleteConsumerGroupOffsets_result_groups(result, out cntp);
  }

  internal static IntPtr AclBinding_new(
    ResourceType restype,
    string name,
    ResourcePatternType resource_pattern_type,
    string principal,
    string host,
    AclOperation operation,
    AclPermissionType permission_type,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._AclBinding_new(restype, name, resource_pattern_type, principal, host, operation, permission_type, errstr, errstr_size);
  }

  internal static IntPtr AclBindingFilter_new(
    ResourceType restype,
    string name,
    ResourcePatternType resource_pattern_type,
    string principal,
    string host,
    AclOperation operation,
    AclPermissionType permission_type,
    StringBuilder errstr,
    UIntPtr errstr_size)
  {
    return Librdkafka._AclBindingFilter_new(restype, name, resource_pattern_type, principal, host, operation, permission_type, errstr, errstr_size);
  }

  internal static void AclBinding_destroy(IntPtr acl_binding)
  {
    Librdkafka._AclBinding_destroy(acl_binding);
  }

  internal static ResourceType AclBinding_restype(IntPtr acl_binding)
  {
    return Librdkafka._AclBinding_restype(acl_binding);
  }

  internal static IntPtr AclBinding_name(IntPtr acl_binding)
  {
    return Librdkafka._AclBinding_name(acl_binding);
  }

  internal static ResourcePatternType AclBinding_resource_pattern_type(IntPtr acl_binding)
  {
    return Librdkafka._AclBinding_resource_pattern_type(acl_binding);
  }

  internal static IntPtr AclBinding_principal(IntPtr acl_binding)
  {
    return Librdkafka._AclBinding_principal(acl_binding);
  }

  internal static IntPtr AclBinding_host(IntPtr acl_binding)
  {
    return Librdkafka._AclBinding_host(acl_binding);
  }

  internal static AclOperation AclBinding_operation(IntPtr acl_binding)
  {
    return Librdkafka._AclBinding_operation(acl_binding);
  }

  internal static AclPermissionType AclBinding_permission_type(IntPtr acl_binding)
  {
    return Librdkafka._AclBinding_permission_type(acl_binding);
  }

  internal static void CreateAcls(
    IntPtr handle,
    IntPtr[] aclBindingsPtrs,
    UIntPtr aclBindingsPtrsSize,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr)
  {
    Librdkafka._CreateAcls(handle, aclBindingsPtrs, aclBindingsPtrsSize, optionsPtr, resultQueuePtr);
  }

  internal static IntPtr CreateAcls_result_acls(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._CreateAcls_result_acls(result, out cntp);
  }

  internal static IntPtr acl_result_error(IntPtr aclres) => Librdkafka._acl_result_error(aclres);

  internal static void DescribeAcls(
    IntPtr handle,
    IntPtr aclBindingFilterPtr,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr)
  {
    Librdkafka._DescribeAcls(handle, aclBindingFilterPtr, optionsPtr, resultQueuePtr);
  }

  internal static IntPtr DescribeAcls_result_acls(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._DescribeAcls_result_acls(result, out cntp);
  }

  internal static void DeleteAcls(
    IntPtr handle,
    IntPtr[] aclBindingFilterPtrs,
    UIntPtr aclBindingFilterPtrsSize,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr)
  {
    Librdkafka._DeleteAcls(handle, aclBindingFilterPtrs, aclBindingFilterPtrsSize, optionsPtr, resultQueuePtr);
  }

  internal static IntPtr DeleteAcls_result_response_error(IntPtr resultResponse)
  {
    return Librdkafka._DeleteAcls_result_response_error(resultResponse);
  }

  internal static IntPtr DeleteAcls_result_responses(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._DeleteAcls_result_responses(result, out cntp);
  }

  internal static IntPtr DeleteAcls_result_response_matching_acls(
    IntPtr resultResponse,
    out UIntPtr matchingAclsCntp)
  {
    return Librdkafka._DeleteAcls_result_response_matching_acls(resultResponse, out matchingAclsCntp);
  }

  internal static IntPtr AlterConsumerGroupOffsets_new(string group, IntPtr partitions)
  {
    return Librdkafka._AlterConsumerGroupOffsets_new(group, partitions);
  }

  internal static void AlterConsumerGroupOffsets_destroy(IntPtr groupPartitions)
  {
    Librdkafka._AlterConsumerGroupOffsets_destroy(groupPartitions);
  }

  internal static void AlterConsumerGroupOffsets(
    IntPtr handle,
    IntPtr[] alterGroupsPartitions,
    UIntPtr alterGroupsPartitionsSize,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr)
  {
    Librdkafka._AlterConsumerGroupOffsets(handle, alterGroupsPartitions, alterGroupsPartitionsSize, optionsPtr, resultQueuePtr);
  }

  internal static IntPtr AlterConsumerGroupOffsets_result_groups(
    IntPtr resultResponse,
    out UIntPtr groupsTopicPartitionsCount)
  {
    return Librdkafka._AlterConsumerGroupOffsets_result_groups(resultResponse, out groupsTopicPartitionsCount);
  }

  internal static IntPtr ListConsumerGroupOffsets_new(string group, IntPtr partitions)
  {
    return Librdkafka._ListConsumerGroupOffsets_new(group, partitions);
  }

  internal static void ListConsumerGroupOffsets_destroy(IntPtr groupPartitions)
  {
    Librdkafka._ListConsumerGroupOffsets_destroy(groupPartitions);
  }

  internal static void ListConsumerGroupOffsets(
    IntPtr handle,
    IntPtr[] listGroupsPartitions,
    UIntPtr listGroupsPartitionsSize,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr)
  {
    Librdkafka._ListConsumerGroupOffsets(handle, listGroupsPartitions, listGroupsPartitionsSize, optionsPtr, resultQueuePtr);
  }

  internal static IntPtr ListConsumerGroupOffsets_result_groups(
    IntPtr resultResponse,
    out UIntPtr groupsTopicPartitionsCount)
  {
    return Librdkafka._ListConsumerGroupOffsets_result_groups(resultResponse, out groupsTopicPartitionsCount);
  }

  internal static void ListConsumerGroups(IntPtr handle, IntPtr optionsPtr, IntPtr resultQueuePtr)
  {
    Librdkafka._ListConsumerGroups(handle, optionsPtr, resultQueuePtr);
  }

  internal static IntPtr ConsumerGroupListing_group_id(IntPtr grplist)
  {
    return Librdkafka._ConsumerGroupListing_group_id(grplist);
  }

  internal static IntPtr ConsumerGroupListing_is_simple_consumer_group(IntPtr grplist)
  {
    return Librdkafka._ConsumerGroupListing_is_simple_consumer_group(grplist);
  }

  internal static ConsumerGroupState ConsumerGroupListing_state(IntPtr grplist)
  {
    return Librdkafka._ConsumerGroupListing_state(grplist);
  }

  internal static IntPtr ListConsumerGroups_result_valid(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._ListConsumerGroups_result_valid(result, out cntp);
  }

  internal static IntPtr ListConsumerGroups_result_errors(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._ListConsumerGroups_result_errors(result, out cntp);
  }

  internal static void DescribeConsumerGroups(
    IntPtr handle,
    [MarshalAs(UnmanagedType.LPArray)] string[] groups,
    UIntPtr groupsCnt,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr)
  {
    Librdkafka._DescribeConsumerGroups(handle, groups, groupsCnt, optionsPtr, resultQueuePtr);
  }

  internal static IntPtr DescribeConsumerGroups_result_groups(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._DescribeConsumerGroups_result_groups(result, out cntp);
  }

  internal static IntPtr ConsumerGroupDescription_group_id(IntPtr grpdesc)
  {
    return Librdkafka._ConsumerGroupDescription_group_id(grpdesc);
  }

  internal static IntPtr ConsumerGroupDescription_error(IntPtr grpdesc)
  {
    return Librdkafka._ConsumerGroupDescription_error(grpdesc);
  }

  internal static int ConsumerGroupDescription_is_simple_consumer_group(IntPtr grpdesc)
  {
    return Librdkafka._ConsumerGroupDescription_is_simple_consumer_group(grpdesc);
  }

  internal static IntPtr ConsumerGroupDescription_partition_assignor(IntPtr grpdesc)
  {
    return Librdkafka._ConsumerGroupDescription_partition_assignor(grpdesc);
  }

  internal static ConsumerGroupState ConsumerGroupDescription_state(IntPtr grpdesc)
  {
    return Librdkafka._ConsumerGroupDescription_state(grpdesc);
  }

  internal static IntPtr ConsumerGroupDescription_coordinator(IntPtr grpdesc)
  {
    return Librdkafka._ConsumerGroupDescription_coordinator(grpdesc);
  }

  internal static IntPtr ConsumerGroupDescription_member_count(IntPtr grpdesc)
  {
    return Librdkafka._ConsumerGroupDescription_member_count(grpdesc);
  }

  internal static IntPtr ConsumerGroupDescription_authorized_operations(
    IntPtr grpdesc,
    out UIntPtr cntp)
  {
    return Librdkafka._ConsumerGroupDescription_authorized_operations(grpdesc, out cntp);
  }

  internal static IntPtr ConsumerGroupDescription_member(IntPtr grpdesc, IntPtr idx)
  {
    return Librdkafka._ConsumerGroupDescription_member(grpdesc, idx);
  }

  internal static IntPtr MemberDescription_client_id(IntPtr member)
  {
    return Librdkafka._MemberDescription_client_id(member);
  }

  internal static IntPtr MemberDescription_group_instance_id(IntPtr member)
  {
    return Librdkafka._MemberDescription_group_instance_id(member);
  }

  internal static IntPtr MemberDescription_consumer_id(IntPtr member)
  {
    return Librdkafka._MemberDescription_consumer_id(member);
  }

  internal static IntPtr MemberDescription_host(IntPtr member)
  {
    return Librdkafka._MemberDescription_host(member);
  }

  internal static IntPtr MemberDescription_assignment(IntPtr member)
  {
    return Librdkafka._MemberDescription_assignment(member);
  }

  internal static IntPtr MemberAssignment_topic_partitions(IntPtr assignment)
  {
    return Librdkafka._MemberAssignment_partitions(assignment);
  }

  internal static IntPtr Node_id(IntPtr node) => Librdkafka._Node_id(node);

  internal static IntPtr Node_host(IntPtr node) => Librdkafka._Node_host(node);

  internal static IntPtr Node_port(IntPtr node) => Librdkafka._Node_port(node);

  internal static IntPtr Node_rack(IntPtr node) => Librdkafka._Node_rack(node);

  internal static void ListOffsets(
    IntPtr handle,
    IntPtr topic_partition_list,
    IntPtr options,
    IntPtr resultQueuePtr)
  {
    Librdkafka._ListOffsets(handle, topic_partition_list, options, resultQueuePtr);
  }

  internal static IntPtr ListOffsets_result_infos(IntPtr resultPtr, out UIntPtr cntp)
  {
    return Librdkafka._ListOffsets_result_infos(resultPtr, out cntp);
  }

  internal static long ListOffsetsResultInfo_timestamp(IntPtr element)
  {
    return Librdkafka._ListOffsetsResultInfo_timestamp(element);
  }

  internal static IntPtr ListOffsetsResultInfo_topic_partition(IntPtr element)
  {
    return Librdkafka._ListOffsetsResultInfo_topic_partition(element);
  }

  internal static ErrorCode topic_result_error(IntPtr topicres)
  {
    return Librdkafka._topic_result_error(topicres);
  }

  internal static IntPtr topic_result_error_string(IntPtr topicres)
  {
    return Librdkafka._topic_result_error_string(topicres);
  }

  internal static IntPtr topic_result_name(IntPtr topicres)
  {
    return Librdkafka._topic_result_name(topicres);
  }

  internal static IntPtr group_result_name(IntPtr groupres)
  {
    return Librdkafka._group_result_name(groupres);
  }

  internal static IntPtr group_result_error(IntPtr groupres)
  {
    return Librdkafka._group_result_error(groupres);
  }

  internal static IntPtr group_result_partitions(IntPtr groupres)
  {
    return Librdkafka._group_result_partitions(groupres);
  }

  internal static void DescribeUserScramCredentials(
    IntPtr handle,
    [MarshalAs(UnmanagedType.LPArray)] string[] users,
    UIntPtr usersCnt,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr)
  {
    Librdkafka._DescribeUserScramCredentials(handle, users, usersCnt, optionsPtr, resultQueuePtr);
  }

  internal static IntPtr DescribeUserScramCredentials_result_descriptions(
    IntPtr event_result,
    out UIntPtr cntp)
  {
    return Librdkafka._DescribeUserScramCredentials_result_descriptions(event_result, out cntp);
  }

  internal static IntPtr UserScramCredentialsDescription_user(IntPtr description)
  {
    return Librdkafka._UserScramCredentialsDescription_user(description);
  }

  internal static IntPtr UserScramCredentialsDescription_error(IntPtr description)
  {
    return Librdkafka._UserScramCredentialsDescription_error(description);
  }

  internal static int UserScramCredentialsDescription_scramcredentialinfo_count(IntPtr description)
  {
    return Librdkafka._UserScramCredentialsDescription_scramcredentialinfo_count(description);
  }

  internal static IntPtr UserScramCredentialsDescription_scramcredentialinfo(
    IntPtr description,
    int i)
  {
    return Librdkafka._UserScramCredentialsDescription_scramcredentialinfo(description, i);
  }

  internal static ScramMechanism ScramCredentialInfo_mechanism(IntPtr scramcredentialinfo)
  {
    return Librdkafka._ScramCredentialInfo_mechanism(scramcredentialinfo);
  }

  internal static int ScramCredentialInfo_iterations(IntPtr scramcredentialinfo)
  {
    return Librdkafka._ScramCredentialInfo_iterations(scramcredentialinfo);
  }

  internal static IntPtr UserScramCredentialUpsertion_new(
    string user,
    ScramMechanism mechanism,
    int iterations,
    byte[] password,
    IntPtr passwordSize,
    byte[] salt,
    IntPtr saltSize)
  {
    return Librdkafka._UserScramCredentialUpsertion_new(user, mechanism, iterations, password, passwordSize, salt, saltSize);
  }

  internal static IntPtr UserScramCredentialDeletion_new(string user, ScramMechanism mechanism)
  {
    return Librdkafka._UserScramCredentialDeletion_new(user, mechanism);
  }

  internal static void UserScramCredentialAlteration_destroy(IntPtr alteration)
  {
    Librdkafka._UserScramCredentialAlteration_destroy(alteration);
  }

  internal static ErrorCode AlterUserScramCredentials(
    IntPtr handle,
    IntPtr[] alterations,
    UIntPtr alterationsCnt,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr)
  {
    return Librdkafka._AlterUserScramCredentials(handle, alterations, alterationsCnt, optionsPtr, resultQueuePtr);
  }

  internal static IntPtr AlterUserScramCredentials_result_responses(
    IntPtr event_result,
    out UIntPtr cntp)
  {
    return Librdkafka._AlterUserScramCredentials_result_responses(event_result, out cntp);
  }

  internal static IntPtr AlterUserScramCredentials_result_response_user(IntPtr element)
  {
    return Librdkafka._AlterUserScramCredentials_result_response_user(element);
  }

  internal static IntPtr AlterUserScramCredentials_result_response_error(IntPtr element)
  {
    return Librdkafka._AlterUserScramCredentials_result_response_error(element);
  }

  internal static void DescribeTopics(
    IntPtr handle,
    IntPtr topicCollectionPtr,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr)
  {
    Librdkafka._DescribeTopics(handle, topicCollectionPtr, optionsPtr, resultQueuePtr);
  }

  internal static IntPtr TopicCollection_of_topic_names([MarshalAs(UnmanagedType.LPArray)] string[] topics, UIntPtr topicsCnt)
  {
    return Librdkafka._TopicCollection_of_topic_names(topics, topicsCnt);
  }

  internal static void TopicCollection_destroy(IntPtr topic_collection)
  {
    Librdkafka._TopicCollection_destroy(topic_collection);
  }

  internal static IntPtr DescribeTopics_result_topics(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._DescribeTopics_result_topics(result, out cntp);
  }

  internal static IntPtr TopicDescription_error(IntPtr topicdesc)
  {
    return Librdkafka._TopicDescription_error(topicdesc);
  }

  internal static IntPtr TopicDescription_name(IntPtr topicdesc)
  {
    return Librdkafka._TopicDescription_name(topicdesc);
  }

  internal static IntPtr TopicDescription_topic_id(IntPtr topicdesc)
  {
    return Librdkafka._TopicDescription_topic_id(topicdesc);
  }

  internal static IntPtr TopicDescription_partitions(IntPtr topicdesc, out UIntPtr cntp)
  {
    return Librdkafka._TopicDescription_partitions(topicdesc, out cntp);
  }

  internal static IntPtr TopicDescription_is_internal(IntPtr topicdesc)
  {
    return Librdkafka._TopicDescription_is_internal(topicdesc);
  }

  internal static IntPtr TopicDescription_authorized_operations(IntPtr topicdesc, out UIntPtr cntp)
  {
    return Librdkafka._TopicDescription_authorized_operations(topicdesc, out cntp);
  }

  internal static IntPtr TopicPartitionInfo_isr(IntPtr topic_partition_info, out UIntPtr cntp)
  {
    return Librdkafka._TopicPartitionInfo_isr(topic_partition_info, out cntp);
  }

  internal static IntPtr TopicPartitionInfo_leader(IntPtr topic_partition_info)
  {
    return Librdkafka._TopicPartitionInfo_leader(topic_partition_info);
  }

  internal static int TopicPartitionInfo_partition(IntPtr topic_partition_info)
  {
    return Librdkafka._TopicPartitionInfo_partition(topic_partition_info);
  }

  internal static IntPtr TopicPartitionInfo_replicas(IntPtr topic_partition_info, out UIntPtr cntp)
  {
    return Librdkafka._TopicPartitionInfo_replicas(topic_partition_info, out cntp);
  }

  internal static void DescribeCluster(IntPtr handle, IntPtr optionsPtr, IntPtr resultQueuePtr)
  {
    Librdkafka._DescribeCluster(handle, optionsPtr, resultQueuePtr);
  }

  internal static IntPtr DescribeCluster_result_nodes(IntPtr result, out UIntPtr cntp)
  {
    return Librdkafka._DescribeCluster_result_nodes(result, out cntp);
  }

  internal static IntPtr DescribeCluster_result_authorized_operations(
    IntPtr result,
    out UIntPtr cntp)
  {
    return Librdkafka._DescribeCluster_result_authorized_operations(result, out cntp);
  }

  internal static IntPtr DescribeCluster_result_controller(IntPtr result)
  {
    return Librdkafka._DescribeCluster_result_controller(result);
  }

  internal static IntPtr DescribeCluster_result_cluster_id(IntPtr result)
  {
    return Librdkafka._DescribeCluster_result_cluster_id(result);
  }

  internal static IntPtr queue_new(IntPtr rk) => Librdkafka._queue_new(rk);

  internal static void queue_destroy(IntPtr rkqu) => Librdkafka._queue_destroy(rkqu);

  internal static IntPtr queue_poll(IntPtr rkqu, int timeout_ms)
  {
    return Librdkafka._queue_poll(rkqu, (IntPtr) timeout_ms);
  }

  internal static void event_destroy(IntPtr rkev) => Librdkafka._event_destroy(rkev);

  internal static IntPtr event_opaque(IntPtr rkev) => Librdkafka._event_opaque(rkev);

  internal static Librdkafka.EventType event_type(IntPtr rkev) => Librdkafka._event_type(rkev);

  internal static ErrorCode event_error(IntPtr rkev) => Librdkafka._event_error(rkev);

  internal static string event_error_string(IntPtr rkev)
  {
    return Util.Marshal.PtrToStringUTF8(Librdkafka._event_error_string(rkev));
  }

  internal static IntPtr event_topic_partition_list(IntPtr rkev)
  {
    return Librdkafka._event_topic_partition_list(rkev);
  }

  internal static ErrorCode error_code(IntPtr error) => Librdkafka._error_code(error);

  internal static string error_string(IntPtr error)
  {
    return Util.Marshal.PtrToStringUTF8(Librdkafka._error_string(error));
  }

  internal static bool error_is_fatal(IntPtr error)
  {
    return Librdkafka._error_is_fatal(error) != IntPtr.Zero;
  }

  internal static bool error_is_retriable(IntPtr error)
  {
    return Librdkafka._error_is_retriable(error) != IntPtr.Zero;
  }

  internal static bool error_txn_requires_abort(IntPtr error)
  {
    return Librdkafka._error_txn_requires_abort(error) != IntPtr.Zero;
  }

  internal static void error_destroy(IntPtr error) => Librdkafka._error_destroy(error);

  internal enum DestroyFlags
  {
    RD_KAFKA_DESTROY_F_NO_CONSUMER_CLOSE = 8,
  }

  internal enum AdminOp
  {
    Any,
    CreateTopics,
    DeleteTopics,
    CreatePartitions,
    AlterConfigs,
    DescribeConfigs,
    DeleteRecords,
    DeleteGroups,
    DeleteConsumerGroupOffsets,
    CreateAcls,
    DescribeAcls,
    DeleteAcls,
    ListConsumerGroups,
    DescribeConsumerGroups,
    ListConsumerGroupOffsets,
    AlterConsumerGroupOffsets,
    IncrementalAlterConfigs,
    DescribeUserScramCredentials,
    AlterUserScramCredentials,
    DescribeTopics,
    DescribeCluster,
    ListOffsets,
  }

  public enum EventType
  {
    None = 0,
    DR = 1,
    Fetch = 2,
    Log = 4,
    Error = 8,
    Rebalance = 16, // 0x00000010
    Offset_Commit = 32, // 0x00000020
    Stats = 64, // 0x00000040
    CreateTopics_Result = 100, // 0x00000064
    DeleteTopics_Result = 101, // 0x00000065
    CreatePartitions_Result = 102, // 0x00000066
    AlterConfigs_Result = 103, // 0x00000067
    DescribeConfigs_Result = 104, // 0x00000068
    DeleteRecords_Result = 105, // 0x00000069
    DeleteGroups_Result = 106, // 0x0000006A
    DeleteConsumerGroupOffsets_Result = 107, // 0x0000006B
    CreateAcls_Result = 1024, // 0x00000400
    DescribeAcls_Result = 2048, // 0x00000800
    DeleteAcls_Result = 4096, // 0x00001000
    ListConsumerGroups_Result = 8192, // 0x00002000
    DescribeConsumerGroups_Result = 16384, // 0x00004000
    ListConsumerGroupOffsets_Result = 32768, // 0x00008000
    AlterConsumerGroupOffsets_Result = 65536, // 0x00010000
    IncrementalAlterConfigs_Result = 131072, // 0x00020000
    DescribeUserScramCredentials_Result = 262144, // 0x00040000
    AlterUserScramCredentials_Result = 524288, // 0x00080000
    DescribeTopics_Result = 1048576, // 0x00100000
    DescribeCluster_Result = 2097152, // 0x00200000
    ListOffsets_Result = 4194304, // 0x00400000
  }

  private static class WindowsNative
  {
    [DllImport("kernel32", SetLastError = true)]
    public static extern IntPtr LoadLibraryEx(
      string lpFileName,
      IntPtr hReservedNull,
      Librdkafka.WindowsNative.LoadLibraryFlags dwFlags);

    [DllImport("kernel32", SetLastError = true)]
    public static extern IntPtr GetModuleHandle(string lpFileName);

    [DllImport("kernel32", SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procname);

    [Flags]
    public enum LoadLibraryFlags : uint
    {
      DONT_RESOLVE_DLL_REFERENCES = 1,
      LOAD_IGNORE_CODE_AUTHZ_LEVEL = 16, // 0x00000010
      LOAD_LIBRARY_AS_DATAFILE = 2,
      LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 64, // 0x00000040
      LOAD_LIBRARY_AS_IMAGE_RESOURCE = 32, // 0x00000020
      LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 512, // 0x00000200
      LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 4096, // 0x00001000
      LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 256, // 0x00000100
      LOAD_LIBRARY_SEARCH_SYSTEM32 = 2048, // 0x00000800
      LOAD_LIBRARY_SEARCH_USER_DIRS = 1024, // 0x00000400
      LOAD_WITH_ALTERED_SEARCH_PATH = 8,
    }
  }

  private static class PosixNative
  {
    [DllImport("libdl")]
    public static extern IntPtr dlopen(string fileName, int flags);

    [DllImport("libdl")]
    public static extern IntPtr dlerror();

    [DllImport("libdl")]
    public static extern IntPtr dlsym(IntPtr handle, string symbol);

    public static string LastError
    {
      get
      {
        IntPtr ptr = Librdkafka.PosixNative.dlerror();
        return ptr == IntPtr.Zero ? "" : System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr);
      }
    }
  }

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void DeliveryReportDelegate(IntPtr rk, IntPtr rkmessage, IntPtr opaque);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void CommitDelegate(IntPtr rk, ErrorCode err, IntPtr offsets, IntPtr opaque);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void ErrorDelegate(IntPtr rk, ErrorCode err, string reason, IntPtr opaque);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void RebalanceDelegate(
    IntPtr rk,
    ErrorCode err,
    IntPtr partitions,
    IntPtr opaque);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void LogDelegate(IntPtr rk, SyslogLevel level, string fac, string buf);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate int StatsDelegate(IntPtr rk, IntPtr json, UIntPtr json_len, IntPtr opaque);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void OAuthBearerTokenRefreshDelegate(
    IntPtr rk,
    IntPtr oauthbearer_config,
    IntPtr opaque);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate int PartitionerDelegate(
    IntPtr rkt,
    IntPtr keydata,
    UIntPtr keylen,
    int partition_cnt,
    IntPtr rkt_opaque,
    IntPtr msg_opaque);

  internal delegate ErrorCode headerGetAllDelegate(
    IntPtr hdrs,
    IntPtr idx,
    out IntPtr namep,
    out IntPtr valuep,
    out IntPtr sizep);

  internal delegate long messageTimestampDelegate(IntPtr rkmessage, out IntPtr tstype);

  internal delegate ErrorCode messageHeadersDelegate(IntPtr rkmessage, out IntPtr hdrsType);

  internal delegate int messageLeaderEpoch(IntPtr rkmessage);

  private delegate ConfRes ConfGet(
    IntPtr conf,
    string name,
    StringBuilder dest,
    ref UIntPtr dest_size);

  private delegate IntPtr ConfDump(IntPtr conf, out UIntPtr cntp);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  private delegate IntPtr ConsumerGroupMetadataWriteDelegate(
    IntPtr cgmd,
    out IntPtr data,
    out IntPtr dataSize);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  private delegate IntPtr ConsumerGroupMetadataReadDelegate(
    out IntPtr cgmd,
    byte[] data,
    IntPtr dataSize);

  private delegate ErrorCode QueryOffsets(
    IntPtr rk,
    string topic,
    int partition,
    out long low,
    out long high,
    IntPtr timeout_ms);

  private delegate ErrorCode GetOffsets(
    IntPtr rk,
    string topic,
    int partition,
    out long low,
    out long high);

  private delegate ErrorCode OffsetsForTimes(IntPtr rk, IntPtr offsets, IntPtr timeout_ms);

  private delegate ErrorCode Subscription(IntPtr rk, out IntPtr topics);

  private delegate ErrorCode Assignment(IntPtr rk, out IntPtr topics);

  private unsafe delegate IntPtr Produceva(IntPtr rk, rd_kafka_vu* vus, IntPtr size);

  private delegate ErrorCode Flush(IntPtr rk, IntPtr timeout_ms);

  private delegate ErrorCode Metadata(
    IntPtr rk,
    bool all_topics,
    IntPtr only_rkt,
    out IntPtr metadatap,
    IntPtr timeout_ms);

  private delegate ErrorCode ListGroups(
    IntPtr rk,
    string group,
    out IntPtr grplistp,
    IntPtr timeout_ms);

  private delegate IntPtr _sasl_set_credentials_delegate(
    IntPtr rk,
    string username,
    string password);

  private delegate IntPtr _CreateTopics_result_topics_delegate(IntPtr result, out UIntPtr cntp);

  private delegate IntPtr _DeleteTopics_result_topics_delegate(IntPtr result, out UIntPtr cntp);

  private delegate IntPtr _DeleteGroups_result_groups_delegate(IntPtr result, out UIntPtr cntp);

  private delegate IntPtr _CreatePartitions_result_topics_delegate(IntPtr result, out UIntPtr cntp);

  private delegate IntPtr _ConfigEntry_synonyms_delegate(IntPtr entry, out UIntPtr cntp);

  private delegate IntPtr _ConfigResource_configs_delegate(IntPtr config, out UIntPtr cntp);

  private delegate IntPtr _AlterConfigs_result_resources_delegate(IntPtr result, out UIntPtr cntp);

  private delegate IntPtr _IncrementalAlterConfigs_result_resources_delegate(
    IntPtr result,
    out UIntPtr cntp);

  private delegate IntPtr _DescribeConfigs_result_resources_delegate(
    IntPtr result,
    out UIntPtr cntp);

  private delegate IntPtr _DeleteConsumerGroupOffsets_result_groups_delegate(
    IntPtr result,
    out UIntPtr cntp);

  private delegate IntPtr _AclBinding_new_delegate(
    ResourceType restype,
    string name,
    ResourcePatternType resource_pattern_type,
    string principal,
    string host,
    AclOperation operation,
    AclPermissionType permission_type,
    StringBuilder errstr,
    UIntPtr errstr_size);

  private delegate IntPtr _AclBindingFilter_new_delegate(
    ResourceType restype,
    string name,
    ResourcePatternType resource_pattern_type,
    string principal,
    string host,
    AclOperation operation,
    AclPermissionType permission_type,
    StringBuilder errstr,
    UIntPtr errstr_size);

  private delegate void _AclBinding_destroy_delegate(IntPtr acl_binding);

  private delegate ResourceType _AclBinding_restype_delegate(IntPtr acl_binding);

  private delegate IntPtr _AclBinding_name_delegate(IntPtr acl_binding);

  private delegate ResourcePatternType _AclBinding_resource_pattern_type_delegate(IntPtr acl_binding);

  private delegate IntPtr _AclBinding_principal_delegate(IntPtr acl_binding);

  private delegate IntPtr _AclBinding_host_delegate(IntPtr acl_binding);

  private delegate AclOperation _AclBinding_operation_delegate(IntPtr acl_binding);

  private delegate AclPermissionType _AclBinding_permission_type_delegate(IntPtr acl_binding);

  private delegate void _CreateAcls_delegate(
    IntPtr handle,
    IntPtr[] aclBindingsPtrs,
    UIntPtr aclBindingsPtrsSize,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr);

  private delegate IntPtr _CreateAcls_result_acls_delegate(IntPtr result, out UIntPtr cntp);

  private delegate IntPtr _acl_result_error_delegate(IntPtr aclres);

  private delegate void _DescribeAcls_delegate(
    IntPtr handle,
    IntPtr aclBindingFilterPtr,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr);

  private delegate IntPtr _DescribeAcls_result_acls_delegate(IntPtr result, out UIntPtr cntp);

  private delegate void _DeleteAcls_delegate(
    IntPtr handle,
    IntPtr[] aclBindingFilterPtrs,
    UIntPtr aclBindingFilterPtrsSize,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr);

  private delegate IntPtr _DeleteAcls_result_response_error_delegate(IntPtr resultResponse);

  private delegate IntPtr _DeleteAcls_result_responses_delegate(IntPtr result, out UIntPtr cntp);

  private delegate IntPtr _DeleteAcls_result_response_matching_acls_delegate(
    IntPtr resultResponse,
    out UIntPtr matchingAclsCntp);

  private delegate IntPtr _AlterConsumerGroupOffsets_new_delegate(string group, IntPtr partitions);

  private delegate void _AlterConsumerGroupOffsets_destroy_delegate(IntPtr groupPartitions);

  private delegate void _AlterConsumerGroupOffsets_delegate(
    IntPtr handle,
    IntPtr[] alterGroupsPartitions,
    UIntPtr alterGroupsPartitionsSize,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr);

  private delegate IntPtr _AlterConsumerGroupOffsets_result_groups_delegate(
    IntPtr resultResponse,
    out UIntPtr groupsTopicPartitionsCount);

  private delegate IntPtr _ListConsumerGroupOffsets_new_delegate(string group, IntPtr partitions);

  private delegate void _ListConsumerGroupOffsets_destroy_delegate(IntPtr groupPartitions);

  private delegate void _ListConsumerGroupOffsets_delegate(
    IntPtr handle,
    IntPtr[] listGroupsPartitions,
    UIntPtr listGroupsPartitionsSize,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr);

  private delegate IntPtr _ListConsumerGroupOffsets_result_groups_delegate(
    IntPtr resultResponse,
    out UIntPtr groupsTopicPartitionsCount);

  private delegate void _ListConsumerGroups_delegate(
    IntPtr handle,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr);

  private delegate IntPtr _ConsumerGroupListing_group_id_delegate(IntPtr grplist);

  private delegate IntPtr _ConsumerGroupListing_is_simple_consumer_group_delegate(IntPtr grplist);

  private delegate ConsumerGroupState _ConsumerGroupListing_state_delegate(IntPtr grplist);

  private delegate IntPtr _ListConsumerGroups_result_valid_delegate(IntPtr result, out UIntPtr cntp);

  private delegate IntPtr _ListConsumerGroups_result_errors_delegate(
    IntPtr result,
    out UIntPtr cntp);

  private delegate void _DescribeConsumerGroups_delegate(
    IntPtr handle,
    [MarshalAs(UnmanagedType.LPArray)] string[] groups,
    UIntPtr groupsCnt,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr);

  private delegate IntPtr _DescribeConsumerGroups_result_groups_delegate(
    IntPtr result,
    out UIntPtr cntp);

  private delegate IntPtr _ConsumerGroupDescription_group_id_delegate(IntPtr grpdesc);

  private delegate IntPtr _ConsumerGroupDescription_error_delegate(IntPtr grpdesc);

  private delegate int _ConsumerGroupDescription_is_simple_consumer_group_delegate(IntPtr grpdesc);

  private delegate IntPtr _ConsumerGroupDescription_partition_assignor_delegate(IntPtr grpdesc);

  private delegate ConsumerGroupState _ConsumerGroupDescription_state_delegate(IntPtr grpdesc);

  private delegate IntPtr _ConsumerGroupDescription_coordinator_delegate(IntPtr grpdesc);

  private delegate IntPtr _ConsumerGroupDescription_member_count_delegate(IntPtr grpdesc);

  private delegate IntPtr _ConsumerGroupDescription_authorized_operations_delegate(
    IntPtr grpdesc,
    out UIntPtr cntp);

  private delegate IntPtr _ConsumerGroupDescription_member_delegate(IntPtr grpdesc, IntPtr idx);

  private delegate IntPtr _MemberDescription_client_id_delegate(IntPtr member);

  private delegate IntPtr _MemberDescription_group_instance_id_delegate(IntPtr member);

  private delegate IntPtr _MemberDescription_consumer_id_delegate(IntPtr member);

  private delegate IntPtr _MemberDescription_host_delegate(IntPtr member);

  private delegate IntPtr _MemberDescription_assignment_delegate(IntPtr member);

  private delegate IntPtr _MemberAssignment_partitions_delegate(IntPtr assignment);

  private delegate IntPtr _Node_id_delegate(IntPtr node);

  private delegate IntPtr _Node_host_delegate(IntPtr node);

  private delegate IntPtr _Node_port_delegate(IntPtr node);

  private delegate IntPtr _Node_rack_delegate(IntPtr node);

  private delegate void _ListOffsets_delegate(
    IntPtr handle,
    IntPtr topic_partition_list,
    IntPtr options,
    IntPtr resultQueuePtr);

  private delegate IntPtr _ListOffsets_result_infos_delegate(IntPtr resultPtr, out UIntPtr cntp);

  private delegate long _ListOffsetsResultInfo_timestamp_delegate(IntPtr element);

  private delegate IntPtr _ListOffsetsResultInfo_topic_partition_delegate(IntPtr element);

  private delegate void _DescribeUserScramCredentials_delegate(
    IntPtr handle,
    [MarshalAs(UnmanagedType.LPArray)] string[] users,
    UIntPtr usersCnt,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr);

  private delegate IntPtr _DescribeUserScramCredentials_result_descriptions_delegate(
    IntPtr event_result,
    out UIntPtr cntp);

  private delegate IntPtr _UserScramCredentialsDescription_user_delegate(IntPtr description);

  private delegate IntPtr _UserScramCredentialsDescription_error_delegate(IntPtr description);

  private delegate int _UserScramCredentialsDescription_scramcredentialinfo_count_delegate(
    IntPtr description);

  private delegate IntPtr _UserScramCredentialsDescription_scramcredentialinfo_delegate(
    IntPtr description,
    int i);

  private delegate ScramMechanism _ScramCredentialInfo_mechanism_delegate(IntPtr scramcredentialinfo);

  private delegate int _ScramCredentialInfo_iterations_delegate(IntPtr scramcredentialinfo);

  private delegate IntPtr _UserScramCredentialUpsertion_new_delegate(
    string user,
    ScramMechanism mechanism,
    int iterations,
    byte[] password,
    IntPtr passwordSize,
    byte[] salt,
    IntPtr saltSize);

  private delegate IntPtr _UserScramCredentialDeletion_new_delegate(
    string user,
    ScramMechanism mechanism);

  private delegate void _UserScramCredentialAlteration_destroy_delegate(IntPtr alteration);

  private delegate ErrorCode _AlterUserScramCredentials_delegate(
    IntPtr handle,
    IntPtr[] alterations,
    UIntPtr alterationsCnt,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr);

  private delegate IntPtr _AlterUserScramCredentials_result_responses_delegate(
    IntPtr event_result,
    out UIntPtr cntp);

  private delegate IntPtr _AlterUserScramCredentials_result_response_user_delegate(IntPtr element);

  private delegate IntPtr _AlterUserScramCredentials_result_response_error_delegate(IntPtr element);

  private delegate void _DescribeTopics_delegate(
    IntPtr handle,
    IntPtr topicCollectionPtr,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr);

  private delegate IntPtr _TopicCollection_of_topic_names_delegate(
    [MarshalAs(UnmanagedType.LPArray)] string[] topics,
    UIntPtr topicsCnt);

  private delegate void _TopicCollection_destroy_delegate(IntPtr topic_collection);

  private delegate IntPtr _DescribeTopics_result_topics_delegate(IntPtr result, out UIntPtr cntp);

  private delegate IntPtr _TopicDescription_error_delegate(IntPtr topicdesc);

  private delegate IntPtr _TopicDescription_name_delegate(IntPtr topicdesc);

  private delegate IntPtr _TopicDescription_topic_id_delegate(IntPtr topicdesc);

  private delegate IntPtr _TopicDescription_partitions_delegate(IntPtr topicdesc, out UIntPtr cntp);

  private delegate IntPtr _TopicDescription_is_internal_delegate(IntPtr topicdesc);

  private delegate IntPtr _TopicDescription_authorized_operations_delegate(
    IntPtr topicdesc,
    out UIntPtr cntp);

  private delegate IntPtr _TopicPartitionInfo_isr_delegate(
    IntPtr topic_partition_info,
    out UIntPtr cntp);

  private delegate IntPtr _TopicPartitionInfo_leader_delegate(IntPtr topic_partition_info);

  private delegate int _TopicPartitionInfo_partition_delegate(IntPtr topic_partition_info);

  private delegate IntPtr _TopicPartitionInfo_replicas_delegate(
    IntPtr topic_partition_info,
    out UIntPtr cntp);

  private delegate void _DescribeCluster_delegate(
    IntPtr handle,
    IntPtr optionsPtr,
    IntPtr resultQueuePtr);

  private delegate IntPtr _DescribeCluster_result_nodes_delegate(IntPtr result, out UIntPtr cntp);

  private delegate IntPtr _DescribeCluster_result_authorized_operations_delegate(
    IntPtr result,
    out UIntPtr cntp);

  private delegate IntPtr _DescribeCluster_result_controller_delegate(IntPtr result);

  private delegate IntPtr _DescribeCluster_result_cluster_id_delegate(IntPtr result);
}
