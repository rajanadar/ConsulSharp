using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Agent.Service
{
    public class AgentServiceModel
    {
        [JsonProperty("ID")]
        public string ServiceId { get; set; }

        public string Service { get; set; }
        public List<string> Tags { get; set; }
        public TaggedAddresses TaggedAddresses { get; set; }

        [JsonProperty("Meta")]
        public Dictionary<string, string> Metadata { get; set; }
        public int Port { get; set; }
        public string Address { get; set; }
        public bool EnableTagOverride { get; set; }

        public Dictionary<string, double> Weights { get; set; }

        public string Kind { get; set; }

        public List<Dictionary<string, object>> Checks { get; set; }

        public Dictionary<string, object> Proxy { get; set; }

        public Dictionary<string, object> Connect { get; set; }

        public string Token { get; set; }

        public string Namespace { get; set; }
    }
}