namespace ConsulSharp.V1.ACL.Agent.Service
{
    public class HealthRequest
    {
        public string ServiceId { get; set; }

        public string ServiceName { get; set; }

        public string Format { get; set; }

        public HealthRequest()
        {
            Format = ContentFormat.JSON;
        }
    }
}