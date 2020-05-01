using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Agent
{
    public class MemberModel
    {
        public string Name { get; set; }

        [JsonProperty("Addr")]
        public string Address { get; set; }

        public int Port { get; set; }

        public Dictionary<string, string> Tags { get; set; }

        public int Status { get; set; }

        public double ProtocolMin { get; set; }

        public double ProtocolMax { get; set; }

        [JsonProperty("ProtocolCur")]
        public double ProtocolCurrent { get; set; }

        public double DelegateMin { get; set; }

        public double DelegateMax { get; set; }

        [JsonProperty("DelegateCur")]
        public double DelegateCurrent { get; set; }
    }
}