namespace ConsulSharp.V1.ACL.Agent
{
    public class ForceLeaveRequest
    {
        public string NodeName { get; set; }
        public bool Prune { get; set; }
    }
}