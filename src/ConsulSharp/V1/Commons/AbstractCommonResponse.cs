using System;
using Newtonsoft.Json;

namespace ConsulSharp.V1.Commons
{
    public abstract class AbstractCommonResponse
    {
        /// <summary>
        /// The ID field in the response is for legacy compatibility and is a copy of the SecretID field. 
        /// New applications should ignore the ID field as it may be removed in a future major Consul version.
        /// </summary>
        [JsonProperty("ID")]
        [Obsolete]
        public string Id { get; set; }

        [JsonProperty("AccessorID")]
        public string AccessorId { get; set; }

        [JsonProperty("SecretID")]
        public string SecretId { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Local")]
        public bool Local { get; set; }

        [JsonProperty("CreateTime")]
        public string CreateTime { get; set; }

        [JsonProperty("Hash")]
        public string Hash { get; set; }

        [JsonProperty("CreateIndex")]
        public int? CreateIndex { get; set; }

        [JsonProperty("ModifyIndex")]
        public int? ModifyIndex { get; set; }
    }
}