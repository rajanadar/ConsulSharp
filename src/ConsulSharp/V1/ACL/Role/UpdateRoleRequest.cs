using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Role
{
    public class UpdateRoleRequest : CreateRoleRequest
    {
        [JsonIgnore]
        public string RoleId { get; set; }
    }
}