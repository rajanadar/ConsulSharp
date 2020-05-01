namespace ConsulSharp.V1.ACL.Agent
{
    public class MaintenanceRequest
    {
        public MaintenanceMode Mode { get; set; }
        public string Reason { get; set; }
    }
}