using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsulSharp.V1.Transaction.Models
{
    /// <summary>
    /// The Generic Transaction Operation.
    /// </summary>
    public class GenericTransactionOperation : Dictionary<string, Dictionary<string, object>>, ITransactionOperation
    {
        /// <summary>
        /// Gets the operation type.
        /// </summary>
        [JsonIgnore]
        public string OperationType { get; }

        /// <summary>
        /// Initializes an instance of <see cref="GenericTransactionOperation"/>.
        /// </summary>
        /// <param name="operationType">The operation type.</param>
        public GenericTransactionOperation(string operationType)
        {
            OperationType = operationType;
        }
    }
}