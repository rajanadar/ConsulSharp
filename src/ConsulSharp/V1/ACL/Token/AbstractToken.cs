using System.Collections.Generic;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Token
{
    /// <summary>
    /// The Abstract Token model.
    /// </summary>
    public abstract class AbstractToken
    {
        [JsonProperty("AccessorID")]
        public string AccessorId { get; set; }

        [JsonProperty("SecretID")]
        public string SecretId { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Policies")]
        public List<IdName> Policies { get; set; }

        [JsonProperty("Local")]
        public bool Local { get; set; }
    }
}