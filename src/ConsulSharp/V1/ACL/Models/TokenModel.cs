using Newtonsoft.Json;

namespace ConsulSharp.V1
{
    /// <summary>
    /// The Token model.
    /// </summary>
    public class TokenModel : AbstractTokenModel
    {
        /// <summary>
        /// The create index.
        /// </summary>
        [JsonProperty("CreateIndex")]
        public int CreateIndex { get; set; }

        /// <summary>
        /// The modify index.
        /// </summary>
        [JsonProperty("ModifyIndex")]
        public int ModifyIndex { get; set; }
    }
}