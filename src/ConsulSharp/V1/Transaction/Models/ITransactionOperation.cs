namespace ConsulSharp.V1.Transaction.Models
{
    /// <summary>
    /// The Transaction Operation interface.
    /// </summary>
    public interface ITransactionOperation
    {
        /// <summary>
        /// The operation type.
        /// </summary>
        string OperationType { get; }
    }
}