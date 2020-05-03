using System.Collections.Generic;
using System.Net;

namespace ConsulSharp.V1.ACL.Agent.Service
{
    public class HealthResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public Dictionary<string, List<AgentServiceModel>> Statuses { get; set; }
        public string PlainText { get; set; }
    }
}