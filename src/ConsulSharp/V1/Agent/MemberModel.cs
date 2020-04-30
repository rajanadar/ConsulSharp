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

        public int ProtocolMin { get; set; }

        public int ProtocolMax { get; set; }

        [JsonProperty("ProtocolCur")]
        public int ProtocolCurrent { get; set; }

        public int DelegateMin { get; set; }

        public int DelegateMax { get; set; }

        [JsonProperty("DelegateCur")]
        public int DelegateCurrent { get; set; }
    }
}