using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Agent
{
    public class ConfigModel
    {
        public string Datacenter { get; set; }
        public string NodeName { get; set; }

        [JsonProperty("NodeID")]
        public string NodeId { get; set; }
        public bool Server { get; set; }
        public string Revision { get; set; }
        public string Version { get; set; }
    }
}