using System;
using System.Collections.Generic;

namespace ConsulSharp.V1.Snapshot.Models
{
    /// <summary>
    /// Request snapshot.
    /// </summary>
    public class SnapshotRequestModel
    {
        /// <summary>
        /// Specifies the datacenter to query. This will default to the datacenter of the agent being queried.
        /// </summary>
        public string Datacenter { get; set; }

        /// <summary>
        /// Specifies that any follower may reply. 
        /// By default requests are forwarded to the leader. 
        /// Followers may be faster to respond, but may have stale data. 
        /// To support bounding the acceptable staleness of snapshots, responses provide the X-Consul-LastContact header 
        /// containing the time in milliseconds that a server was last contacted by the leader node. 
        /// The X-Consul-KnownLeader header also indicates if there is a known leader. 
        /// These can be used by clients to gauge the staleness of a snapshot and take appropriate action. 
        /// The stale mode is particularly useful for taking a snapshot of a cluster in a failed state with no current leader.
        /// </summary>
        public bool Stale { get; set; }

        internal string ToQueryString()
        {
            var list = new List<string>();

            if (!string.IsNullOrWhiteSpace(Datacenter))
            {
                list.Add("dc=" + Datacenter);
            }
            
            if (Stale)
            {
                list.Add("stale");
            }

            if (list.Count > 0)
            {
                return "?" + string.Join("&", list);
            }

            return string.Empty;
        }
    }
}