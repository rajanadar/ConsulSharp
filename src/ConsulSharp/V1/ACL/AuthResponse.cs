using System.Collections.Generic;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL
{
    public class AuthResponse : AbstractCommonResponse
    {
        [JsonProperty("Roles")]
        public List<IdName> Roles { get; set; }

        [JsonProperty("ServiceIdentities")]
        public List<ServiceIdentity> ServiceIdentities { get; set; }
    }
}