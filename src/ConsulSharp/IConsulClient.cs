using ConsulSharp.V1;

namespace ConsulSharp
{
    /// <summary>
    /// Provides an interface to interact with Consul as a client.
    /// This is the only entry point for consuming the Consul Client.
    /// </summary>
    public interface IConsulClient
    {
        /// <summary>
        /// Gets the Consul Client Settings.
        /// </summary>
        ConsulClientSettings Settings { get; }

        /// <summary>
        /// Gets the V1 Client interface for Consul Api.
        /// </summary>
        IConsulClientV1 V1 { get; }
    }
}

