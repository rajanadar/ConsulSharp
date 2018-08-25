using ConsulSharp.Core;
using ConsulSharp.V1;

namespace ConsulSharp
{
    /// <summary>
    /// The concrete Consul client class.
    /// </summary>
    public class ConsulClient : IConsulClient
    {
        private readonly Polymath polymath;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="consulClientSettings"></param>
        public ConsulClient(ConsulClientSettings consulClientSettings)
        {
            polymath = new Polymath(consulClientSettings);
            V1 = new ConsulClientV1(polymath);
        }

        /// <summary>
        /// Gets the V1 Client interface for Consul Api.
        /// </summary>
        public IConsulClientV1 V1 { get; }

        /// <summary>
        /// Gets the Consul Client Settings.
        /// </summary>
        public ConsulClientSettings Settings => polymath.ConsulClientSettings;
    }
}
