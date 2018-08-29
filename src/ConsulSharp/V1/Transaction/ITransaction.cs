using System.Threading.Tasks;
using ConsulSharp.V1.Commons;
using ConsulSharp.V1.Transaction.Models;

namespace ConsulSharp.V1.Transaction
{
    /// <summary>
    /// The Transaction interface.
    /// </summary>
    public interface ITransaction
    {
        /// <summary>
        /// This endpoint permits submitting a list of operations to apply to the KV store inside of a transaction. 
        /// If any operation fails, the transaction is rolled back and none of the changes are applied.
        /// If the transaction does not contain any write operations then it will be fast-pathed internally to an 
        /// endpoint that works like other reads, except that blocking queries are not currently supported.
        /// In this mode, you may supply the ?stale or ?consistent query parameters with the request to control consistency.
        /// To support bounding the acceptable staleness of data, read-only transaction responses provide the 
        /// X-Consul-LastContact header containing the time in milliseconds that a server was last contacted by 
        /// the leader node.The X-Consul-KnownLeader header also indicates if there is a known leader.
        /// These won't be present if the transaction contains any write operations, and any consistency query 
        /// parameters will be ignored, since writes are always managed by the leader via the Raft consensus protocol.
        /// </summary>
        /// <param name="request">
        /// The transaction request.
        /// A list of operations to perform inside the atomic transaction. 
        /// Up to 64 operations may be present in a single transaction.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        Task<ConsulResponse<TransactionResponseModel>> CreateAsync(ConsulRequest<TransactionRequestModel> request);
    }
}