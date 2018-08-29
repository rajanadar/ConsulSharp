using Newtonsoft.Json;

namespace ConsulSharp.V1.Transaction.Models
{
    /// <summary>
    /// The transaction result interface.
    /// </summary>
    [JsonConverter(typeof(TransactionResultJsonConverter))]
    public interface ITransactionResult
    {
        /// <summary>
        /// The operation type.
        /// </summary>
        string OperationType { get; }
    }
}