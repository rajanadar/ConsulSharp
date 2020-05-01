using System.Collections.Generic;

namespace ConsulSharp.V1.ACL.Agent
{
    public class Counter
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int Sum { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Mean { get; set; }
        public int Stddev { get; set; }

        public Dictionary<string, object> Labels { get; set; }
}
}