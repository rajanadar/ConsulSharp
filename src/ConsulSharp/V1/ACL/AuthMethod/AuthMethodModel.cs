﻿using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.AuthMethod
{
    /// <summary>
    /// AuthMethod Model.
    /// </summary>
    public class AuthMethodModel : AbstractAuthMethod
    {
        [JsonProperty("CreateIndex")]
        public ulong? CreateIndex { get; set; }

        [JsonProperty("ModifyIndex")]
        public ulong? ModifyIndex { get; set; }
    }
}