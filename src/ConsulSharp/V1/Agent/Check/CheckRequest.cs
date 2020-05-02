using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Agent.Check
{
    public class CheckRequest
    {
        public string Name { get; set; }

        [JsonProperty("ID")]
        public string CheckId { get; set; }

        public string Interval { get; set; }

        public string Notes { get; set; }     
        
        public string DeregisterCriticalServiceAfter { get; set; }

        [JsonProperty("Args")]
        public List<string> Arguments { get; set; }

        public string AliasNode { get; set; }

        public string AliasService { get; set; }

        [JsonProperty("DockerContainerID")]
        public string DockerContainerId { get; set; }

        public string GRPC { get; set; }

        public bool GRPCUseTLS { get; set; }

        public string HTTP { get; set; }

        public string Method { get; set; }

        public string Body { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public string Timeout { get; set; }

        public long? OutputMaxSize { get; set; }

        public bool TLSSkipVerify { get; set; }

        public string TCP { get; set; }

        [JsonProperty("TTL")]
        public string TimeToLive { get; set; }

        [JsonProperty("ServiceID")]
        public string ServiceId { get; set; }

        public string Status { get; set; }

        public string Shell { get; set; }
    }
}