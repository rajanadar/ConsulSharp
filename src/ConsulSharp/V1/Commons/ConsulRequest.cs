namespace ConsulSharp.V1.Commons
{
    /// <summary>
    /// Represents a Consul request.
    /// </summary>
    public class ConsulRequest
    {
        /// <summary>
        /// Gets or sets a flag indicating if the request should be blocking.
        /// </summary>
        public bool? Blocking { get; set; }

        /// <summary>
        /// Most of the read query endpoints support multiple levels of consistency. 
        /// Since no policy will suit all clients' needs, these consistency modes allow the user to have the ultimate say 
        /// in how to balance the trade-offs inherent in a distributed system.
        /// </summary>
        public ConsistencyMode? ConsistencyMode { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier representing the current state of the requested resource for blocking queries.
        /// </summary>
        public ulong? Index { get; set; }

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
        /// A filter expression is used to refine a data query for some API listing 
        /// endpoints as notated in the individual API documentation. 
        /// Filtering will be executed on the Consul server before data is returned, 
        /// reducing the network load. 
        /// </summary>
        public string FilterExpression { get; set; }

        /// <summary>
        /// Last hash header value seen
        /// </summary>
        public string ContentHash { get; set; }

        /// <summary>
        /// Used to enable cached responses.
        /// </summary>
        public bool? Cached { get; set; }

        /// <summary>
        /// Used to request better freshness of data.
        /// </summary>
        public int? CacheControlMaxAgeSeconds { get; set; }

        /// <summary>
        /// Allows clients to maintain fresh results in normal operation but allow stale ones 
        /// if the servers are unavailable.
        /// </summary>
        public int? CacheControlStaleIfErrorSeconds { get; set; }

        /// <summary>
        /// Allows clients to have always-fresh results yet keeping the 
        /// cache populated with the most recent result.
        /// </summary>
        public bool? CacheControlMustRevalidate { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the response json should be pretty.
        /// </summary>
        public bool? PrettyJsonResponse { get; set; }

        /// <summary>
        /// The namespace to use to achieve tenant level isolation.
        /// </summary>
        public string PerRequestNamespace { get; set; }

        /// <summary>
        /// Builder to easily get the consul request if you only mean to provide the data.
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public static ConsulRequest<TRequestData> WithData<TRequestData>(TRequestData requestData)
        {
            return new ConsulRequest<TRequestData>
            {
                RequestData = requestData
            };
        }
    }
}