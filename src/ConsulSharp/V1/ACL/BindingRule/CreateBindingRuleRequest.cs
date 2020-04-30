using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.BindingRule
{
    public class CreateBindingRuleRequest : AbstractBindingRule
    {
        [JsonProperty("Namespace")]
        public string Namespace { get; set; }
    }
}