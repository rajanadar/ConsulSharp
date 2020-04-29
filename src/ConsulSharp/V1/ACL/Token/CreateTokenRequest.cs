using System.Collections.Generic;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Token
{
    public class CreateTokenRequest : AbstractToken
    {
        [JsonProperty("Roles")]
        public List<IdName> Roles { get; set; }

        [JsonProperty("ServiceIdentities")]
        public List<ServiceIdentity> ServiceIdentities { get; set; }

        [JsonProperty("ExpirationTime")]
        public string ExpirationTime { get; set; }

        [JsonProperty("ExpirationTTL")]
        public string ExpirationTTL { get; set; }

        [JsonProperty("Namespace")]
        public string Namespace { get; set; }
    }
}