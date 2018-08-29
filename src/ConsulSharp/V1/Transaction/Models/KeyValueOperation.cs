using Newtonsoft.Json;

namespace ConsulSharp.V1.Transaction.Models
{
    /// <summary>
    /// The KV operation type.
    /// </summary>
    public class KeyValueOperation
    {
        /// <summary>
        /// Gets or sets the Verb. Use <see cref="OperationVerbs"/> to set this.
        /// </summary>
        [JsonProperty("Verb")]
        public string Verb { get; set; }

        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        [JsonProperty("Key")]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the Base64 Encoded Value.
        /// </summary>
        [JsonProperty("Value")]
        public string Base64EncodedValue { get; set; }

        /// <summary>
        /// Gets or sets the Flags.
        /// </summary>
        [JsonProperty("Flags")]
        public uint Flags { get; set; }

        /// <summary>
        /// Gets or sets the Index.
        /// </summary>
        [JsonProperty("Index")]
        public ulong Index { get; set; }

        /// <summary>
        /// Gets or sets the Session.
        /// </summary>
        [JsonProperty("Session")]
        public string Session { get; set; }
    }
}