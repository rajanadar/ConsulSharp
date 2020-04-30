using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.AuthMethod
{
    public class CreateAuthMethodRequest : AbstractAuthMethod
    {
        [JsonProperty("Namespace")]
        public string Namespace { get; set; }
    }
}