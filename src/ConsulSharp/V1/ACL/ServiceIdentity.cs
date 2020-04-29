using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL
{
    public class ServiceIdentity
    {
        [JsonProperty("ServiceName")]
        public string ServiceName { get; set; }
    }
}