using System.Collections.Generic;

namespace ConsulSharp.V1.ACL.Agent.Service
{
    public class HealthResponse
    {
        public Dictionary<string, List<AgentServiceModel>> Statuses { get; set; }
        public string PlainText { get; set; }
    }
}