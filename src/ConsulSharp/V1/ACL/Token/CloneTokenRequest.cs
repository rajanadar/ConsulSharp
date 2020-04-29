using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Token
{
    public class CloneTokenRequest
    {
        [JsonIgnore]
        public string AccessorId { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Namespace")]
        public string Namespace { get; set; }
    }
}