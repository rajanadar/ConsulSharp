using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.BindingRule
{
    public class UpdateBindingRuleRequest : CreateBindingRuleRequest
    {
        [JsonIgnore]
        public string BindingRuleId { get; set; }
    }
}