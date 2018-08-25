namespace ConsulSharp.V1.Commons
{
    /// <summary>
    /// Represents a Consul request.
    /// </summary>
    /// <typeparam name="TRequestData">The type of the data contained in the request.</typeparam>
    public class Request<TRequestData> : Request
    {
        /// <summary>
        /// Gets or sets the request data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public TRequestData RequestData { get; set; }
    }
}