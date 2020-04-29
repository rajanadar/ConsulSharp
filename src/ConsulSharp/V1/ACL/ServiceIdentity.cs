using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL
{
    public class ServiceIdentity
    {
        [JsonProperty("ServiceName")]
        public string ServiceName { get; set; }

        [JsonProperty("Datacenters")]
        public List<string> Datacenters { get; set; }
    }
}