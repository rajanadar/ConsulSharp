using System;

namespace ConsulSharp.V1.Commons
{
    /// <summary>
    /// Represents a Consul response.
    /// </summary>
    public class ConsulResponse
    {
        /// <summary>
        /// Gets or sets the time in milliseconds that a server was last contacted by the leader node.
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier representing the current state of the requested resource for blocking queries.
        /// </summary>
        public string LastContactMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets the known leader.
        /// </summary>
        public bool? KnownLeader { get; set; }
    }
}