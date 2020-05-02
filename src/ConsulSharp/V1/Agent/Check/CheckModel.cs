using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Agent.Check
{
    public class CheckModel
    {
        public string Node { get; set; }

        [JsonProperty("CheckID")]
        public string CheckId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string Output { get; set; }

        [JsonProperty("ServiceID")]
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public List<string> ServiceTags { get; set; }
    }
}