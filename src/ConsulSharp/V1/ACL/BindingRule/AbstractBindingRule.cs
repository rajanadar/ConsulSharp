using System.Collections.Generic;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.BindingRule
{
    public abstract class AbstractBindingRule
    {
        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("AuthMethod")]
        public string AuthMethod { get; set; }

        [JsonProperty("Selector")]
        public string Selector { get; set; }

        [JsonProperty("BindType")]
        public string BindType { get; set; }

        [JsonProperty("BindName")]
        public string BindName { get; set; }
    }
}