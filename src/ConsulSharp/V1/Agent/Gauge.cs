using System.Collections.Generic;

namespace ConsulSharp.V1.ACL.Agent
{
    public class Gauge
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Dictionary<string, object> Labels { get; set; }
    }
}