using System.Collections.Generic;

namespace ConsulSharp.V1.ACL.Agent
{
    public class Counter
    {
        public string Name { get; set; }
        public double Count { get; set; }
        public double Sum { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Mean { get; set; }
        public double Stddev { get; set; }

        public Dictionary<string, object> Labels { get; set; }
}
}