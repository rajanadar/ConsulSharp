﻿using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Models
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
        public long CreateIndex { get; set; }

        /// <summary>
        /// The modify index.
        /// </summary>
        [JsonProperty("ModifyIndex")]
        public long ModifyIndex { get; set; }
    }
}