using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Role
{
    public class CreateRoleRequest : AbstractRole
    {
        [JsonProperty("Namespace")]
        public string Namespace { get; set; }
    }
}