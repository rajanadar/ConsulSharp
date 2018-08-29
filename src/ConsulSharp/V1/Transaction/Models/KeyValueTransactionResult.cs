using ConsulSharp.V1.KeyValue.Models;
using Newtonsoft.Json;

namespace ConsulSharp.V1.Transaction.Models
{
    /// <summary>
    /// The key value transaction result.
    /// </summary>
    public class KeyValueTransactionResult : ITransactionResult
    {
        /// <summary>
        /// The operation type.
        /// </summary>
        [JsonIgnore]
        public string OperationType => "KV";

        /// <summary>
        /// The Key Value model.
        /// </summary>
        public KeyValueModel KeyValueModel { get; set; }
    }
}