using Newtonsoft.Json;

namespace ConsulSharp.V1.Transaction.Models
{
    /// <summary>
    /// The transaction error.
    /// </summary>
    public class TransactionError
    {
        /// <summary>
        /// Ihe index of the failed operation in the transaction.
        /// </summary>
        [JsonProperty("OpIndex")]
        public ulong OperationIndex { get; set; }

        /// <summary>
        /// A string with an error message about why that operation failed.
        /// </summary>
        [JsonProperty("What")]
        public string Message { get; set; }
    }
}