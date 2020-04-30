using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Policy
{
    public abstract class AbstractPolicy
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Rules")]
        public string Rules { get; set; }

        [JsonProperty("Datacenters")]
        public List<string> Datacenters { get; set; }
    }
}