using System.Collections.Generic;

namespace ConsulSharp.V1.ACL.Agent
{
    public class MetricsModel
    {
        public string Timestamp { get; set; }
        public List<Gauge> Gauges { get; set; }
        public List<Point> Points { get; set; }
        public List<Counter> Counters { get; set; }
        public List<Counter> Samples { get; set; }
    }
}