namespace ConsulSharp.V1.ACL.Agent.Service
{
    public class MaintenanceRequest
    {
        public string ServiceId { get; set; }
        public MaintenanceMode Mode { get; set; }
        public string Reason { get; set; }
    }
}