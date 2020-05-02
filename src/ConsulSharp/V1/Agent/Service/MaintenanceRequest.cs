namespace ConsulSharp.V1.ACL.Agent.Service
{
    public class MaintenanceRequest
    {
        public MaintenanceMode Mode { get; set; }
        public string Reason { get; set; }
    }
}