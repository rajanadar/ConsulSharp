using Newtonsoft.Json;

namespace ConsulSharp.V1
{
    /// <summary>
    /// The Replication Status Model.
    /// </summary>
    public class ReplicationStatusModel
    {
        /// <summary>
        /// Reports whether ACL replication is enabled for the datacenter.
        /// </summary>
        [JsonProperty("Enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Reports whether the ACL replication process is running. 
        /// The process may take approximately 60 seconds to begin running after a leader election occurs.
        /// </summary>
        [JsonProperty("Running")]
        public bool Running { get; set; }

        /// <summary>
        /// The authoritative ACL datacenter that ACLs are being replicated from, and will match the acl_datacenter configuration.
        /// </summary>
        [JsonProperty("SourceDatacenter")]
        public string SourceDatacenter { get; set; }

        /// <summary>
        /// The last index that was successfully replicated. 
        /// You can compare this to the X-Consul-Index header returned by the /v1/acl/list endpoint to 
        /// determine if the replication process has gotten all available ACLs. 
        /// Replication runs as a background process approximately every 30 seconds, 
        /// and that local updates are rate limited to 100 updates/second, 
        /// so so it may take several minutes to perform the initial sync of a large set of ACLs. 
        /// After the initial sync, replica lag should be on the order of about 30 seconds.
        /// </summary>
        [JsonProperty("ReplicatedIndex")]
        public int ReplicatedIndex { get; set; }

        /// <summary>
        /// The UTC time of the last successful sync operation. 
        /// Since ACL replication is done with a blocking query, 
        /// this may not update for up to 5 minutes if there have been no ACL changes to replicate. 
        /// A zero value of "0001-01-01T00:00:00Z" will be present if no sync has been successful.
        /// </summary>
        [JsonProperty("LastSuccess")]
        public string LastSuccessTime { get; set; }

        /// <summary>
        /// The UTC time of the last error encountered during a sync operation. 
        /// If this time is later than LastSuccess, you can assume the replication process is not in a good state. 
        /// A zero value of "0001-01-01T00:00:00Z" will be present if no sync has resulted in an error.
        /// </summary>
        [JsonProperty("LastError")]
        public string LastErrorTime { get; set; }
    }
}