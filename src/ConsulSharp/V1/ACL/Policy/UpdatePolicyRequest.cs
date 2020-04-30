using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Policy
{
    public class UpdatePolicyRequest : CreatePolicyRequest
    {
        [JsonIgnore]
        public string PolicyId { get; set; }
    }
}