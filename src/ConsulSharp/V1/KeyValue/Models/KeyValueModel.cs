using Newtonsoft.Json;

namespace ConsulSharp.V1.KeyValue.Models
{
    /// <summary>
    /// Key value model.
    /// </summary>
    public class KeyValueModel
    {
        /// <summary>
        /// The internal index value that represents when the entry was created.
        /// </summary>
        [JsonProperty("CreateIndex")]
        public long CreateIndex { get; set; }

        /// <summary>
        /// The last index that modified this key. 
        /// This index corresponds to the X-Consul-Index header value that is returned in responses, 
        /// and it can be used to establish blocking queries by setting the ?index query parameter. 
        /// You can even perform blocking queries against entire subtrees of the KV store: if ?recurse is provided, 
        /// the returned X-Consul-Index corresponds to the latest ModifyIndex within the prefix, and a blocking query 
        /// using that ?index will wait until any key within that prefix is updated.
        /// </summary>
        [JsonProperty("ModifyIndex")]
        public long ModifyIndex { get; set; }

        /// <summary>
        /// The number of times this key has successfully been acquired in a lock. 
        /// If the lock is held, the Session key provides the session that owns the lock.
        /// </summary>
        [JsonProperty("LockIndex")]
        public long LockIndex { get; set; }

        /// <summary>
        /// Simply the full path of the entry.
        /// </summary>
        [JsonProperty("Key")]
        public string Key { get; set; }

        /// <summary>
        /// An opaque unsigned integer that can be attached to each entry. 
        /// Clients can choose to use this however makes sense for their application.
        /// </summary>
        [JsonProperty("Flags")]
        public uint Flags { get; set; }

        /// <summary>
        /// A base64-encoded blob of data.
        /// </summary>
        [JsonProperty("Value")]
        public string Base64EncodedValue { get; set; }

        /// <summary>
        /// Session id.
        /// </summary>
        [JsonProperty("Session")]
        public string SessionId { get; set; }
    }
}