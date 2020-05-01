using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Agent
{
    public class CoordinateModel
    {
        public double Adjustment { get; set; }
        public double Error { get; set; }
        
        [JsonProperty("Vec")]
        public List<double> Vectors { get; set; }
    }
}