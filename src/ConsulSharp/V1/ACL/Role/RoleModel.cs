using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Role
{
    /// <summary>
    /// Role Model.... role model, haaaaa!
    /// </summary>
    public class RoleModel : AbstractRole
    {
        [JsonProperty("ID")]
        public string RoleId { get; set; }

        [JsonProperty("Hash")]
        public string Hash { get; set; }

        [JsonProperty("CreateIndex")]
        public ulong? CreateIndex { get; set; }

        [JsonProperty("ModifyIndex")]
        public ulong? ModifyIndex { get; set; }
    }
}