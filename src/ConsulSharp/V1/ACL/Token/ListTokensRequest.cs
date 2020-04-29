namespace ConsulSharp.V1.ACL.Token
{
    public class ListTokensRequest
    {
        public string PolicyId { get; set; }

        public string RoleId { get; set; }

        public string AuthMethod { get; set; }

        public string AuthMethodNamespace { get; set; }
    }
}