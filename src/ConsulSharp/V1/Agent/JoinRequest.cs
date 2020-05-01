namespace ConsulSharp.V1.ACL.Agent
{
    public class JoinRequest
    {
        public string AgentAddress { get; set; }

        public bool OverWAN { get; set; }
    }
}