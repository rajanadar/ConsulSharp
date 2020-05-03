using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Agent.Service
{
    public class AddressPort
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }
    }
}