using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Policy
{
    public class PolicyModel : AbstractPolicy
    {
        [JsonProperty("ID")]
        public string PolicyId { get; set; }

        [JsonProperty("Hash")]
        public string Hash { get; set; }

        [JsonProperty("CreateIndex")]
        public ulong? CreateIndex { get; set; }

        [JsonProperty("ModifyIndex")]
        public ulong? ModifyIndex { get; set; }
    }
}