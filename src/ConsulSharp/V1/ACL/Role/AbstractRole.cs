using System.Collections.Generic;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Role
{
    public abstract class AbstractRole
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Policies")]
        public List<IdName> Policies { get; set; }

        [JsonProperty("ServiceIdentities")]
        public List<ServiceIdentity> ServiceIdentities { get; set; }
    }
}