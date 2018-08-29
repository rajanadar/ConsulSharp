using System.Collections.Generic;
using ConsulSharp.V1.KeyValue;
using Newtonsoft.Json;

namespace ConsulSharp.V1.Transaction.Models
{
    /// <summary>
    /// The Transaction Response Model.
    /// </summary>
    public class TransactionResponseModel
    {
        /// <summary>
        /// The result.
        /// If the transaction can be processed, a status code of <see cref="TransactionStatus.Success"/> will be returned if it was successfully applied.
        /// Or a status code of <see cref="TransactionStatus.Rollback"/> will be returned if it was rolled back.
        /// </summary>
        [JsonIgnore]
        public TransactionStatus Status { get; set; }

        /// <summary>
        /// Has entries for some operations if the transaction was successful. 
        /// To save space, the Value will be null for any Verb other than "get" or "get-tree". 
        /// Like the <see cref="IKeyValue.WriteAsync(Commons.ConsulRequest{KeyValue.Models.WriteKeyValueModel})"/> endpoint, Value will be Base64-encoded if it is present. 
        /// Also, no result entries will be added for verbs that delete keys.
        /// </summary>
        public List<ITransactionResult> Results { get; set; }

        /// <summary>
        /// Has entries describing which operations failed if the transaction was rolled back. 
        /// The OpIndex gives the index of the failed operation in the transaction, 
        /// and What is a string with an error message about why that operation failed.
        /// </summary>
        public List<TransactionError> Errors { get; set; }
    }
}