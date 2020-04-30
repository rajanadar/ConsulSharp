using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Agent
{
    public class ConfigAndMemberModel
    {
        public ConfigModel Config { get; set; } 

        public Dictionary<string, object> DebugConfig { get; set; }

        [JsonProperty("Coord")]
        public CoordinateModel Coordinates { get; set; }

        // raja todo
        // add member for stats. see output
        
        public MemberModel Member { get; set; }

        [JsonProperty("Meta")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}