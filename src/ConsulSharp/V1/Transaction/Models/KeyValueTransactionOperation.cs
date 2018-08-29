using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsulSharp.V1.Transaction.Models
{
    /// <summary>
    /// The KeyValue Transaction Operation.
    /// </summary>
    public class KeyValueTransactionOperation : ITransactionOperation
    {
        /// <summary>
        /// The Key Value operation type.
        /// </summary>
        [JsonProperty("KV")]
        public KeyValueOperation KeyValueOperation { get; set; }

        /// <summary>
        /// Key Value operation type.
        /// </summary>
        [JsonIgnore]
        public string OperationType => "KV";
    }
}