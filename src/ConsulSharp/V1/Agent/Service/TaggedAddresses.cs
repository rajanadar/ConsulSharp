using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Agent.Service
{
    public class TaggedAddresses
    {
        [JsonProperty("lan")]
        public AddressPort LAN { get; set; }

        [JsonProperty("wan")]
        public AddressPort WAN { get; set; }
    }
}