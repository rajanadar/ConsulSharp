using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Agent.Check
{
    public class StatusRequest
    {
        [JsonIgnore]
        public string CheckId { get; set; }

        public string Status { get; set; }

        public string Output { get; set; }
    }
}