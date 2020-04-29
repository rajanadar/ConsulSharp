using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.LegacyToken
{
    /// <summary>
    /// The Token model.
    /// </summary>
    public class TokenModel : AbstractToken
    {
        /// <summary>
        /// The create index.
        /// </summary>
        [JsonProperty("CreateIndex")]
        public ulong CreateIndex { get; set; }

        /// <summary>
        /// The modify index.
        /// </summary>
        [JsonProperty("ModifyIndex")]
        public ulong ModifyIndex { get; set; }
    }
}