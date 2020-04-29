using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Token
{
    public class UpdateTokenRequest : CreateTokenRequest
    {
        [JsonProperty("AuthMethod")]
        public string AuthMethod { get; set; }
    }
}