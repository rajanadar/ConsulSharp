using System.Collections.Generic;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL
{
    /// <summary>
    /// The BootstrapResponse model.
    /// </summary>
    public class BootstrapResponse : AbstractCommonResponse
    {
        [JsonProperty("Policies")]
        public List<IdName> Policies { get; set; }
    }
}