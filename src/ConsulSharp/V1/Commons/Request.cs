namespace ConsulSharp.V1.Commons
{
    /// <summary>
    /// Represents a Consul request.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Most of the read query endpoints support multiple levels of consistency. 
        /// Since no policy will suit all clients' needs, these consistency modes allow the user to have the ultimate say 
        /// in how to balance the trade-offs inherent in a distributed system.
        /// </summary>
        public ConsistencyMode ConsistencyMode { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier representing the current state of the requested resource for blocking queries.
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets a maximum duration for the blocking request. 
        /// This is limited to 10 minutes. 
        /// If not set, the wait time defaults to 5 minutes. 
        /// This value can be specified in the form of "10s" or "5m" (i.e., 10 seconds or 5 minutes, respectively). 
        /// A small random amount of additional wait time is added to the supplied maximum wait time to spread 
        /// out the wake up time of any concurrent requests. 
        /// This adds up to wait / 16 additional time to the maximum duration.
        /// </summary>
        public string Wait { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the response json should be pretty.
        /// </summary>
        public bool PrettyJsonResponse { get; set; }
    }
}