using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.AuthMethod
{
    public abstract class AbstractAuthMethod
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Config")]
        public Dictionary<string, string> Config { get; set; }
    }
}