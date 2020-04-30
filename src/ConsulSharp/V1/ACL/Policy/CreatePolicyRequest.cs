using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Policy
{
    public class CreatePolicyRequest : AbstractPolicy
    {
        [JsonProperty("Namespace")]
        public string Namespace { get; set; }
    }
}