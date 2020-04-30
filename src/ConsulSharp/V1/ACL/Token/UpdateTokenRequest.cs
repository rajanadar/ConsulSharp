using System.Collections.Generic;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Token
{
    public class UpdateTokenRequest : AbstractToken
    {
        [JsonProperty("Roles")]
        public List<IdName> Roles { get; set; }

        [JsonProperty("ServiceIdentities")]
        public List<ServiceIdentity> ServiceIdentities { get; set; }

        [JsonProperty("AuthMethod")]
        public string AuthMethod { get; set; }

        [JsonProperty("ExpirationTime")]
        public string ExpirationTime { get; set; }
    }
}