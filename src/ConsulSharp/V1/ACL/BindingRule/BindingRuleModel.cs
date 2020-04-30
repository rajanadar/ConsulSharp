using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.BindingRule
{
    /// <summary>
    /// BindingRule Model.
    /// </summary>
    public class BindingRuleModel : AbstractBindingRule
    {
        [JsonProperty("ID")]
        public string BindingRuleId { get; set; }

        [JsonProperty("CreateIndex")]
        public ulong? CreateIndex { get; set; }

        [JsonProperty("ModifyIndex")]
        public ulong? ModifyIndex { get; set; }
    }
}