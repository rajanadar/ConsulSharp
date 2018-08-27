using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;
using ConsulSharp.V1.KeyValue.Models;

namespace ConsulSharp.V1.KeyValue
{
    /// <summary>
    /// The ACL interface.
    /// </summary>
    public interface IKeyValue
    {
        /// <summary>
        /// Creates or updates a Key with values.
        /// </summary>
        /// <param name="request">The request with key options.</param>
        /// <returns>Either true or false, indicating whether the create/update succeeded.</returns>
        Task<ConsulResponse<bool>> WriteAsync(ConsulRequest<WriteKeyValueModel> request);

        /// <summary>
        /// Reads a key value.
        /// </summary>
        /// <param name="request">The request with key options.</param>
        /// <returns>The key value data in one of several formats.</returns>
        Task<ConsulResponse<KeyValueData>> ReadAsync(ConsulRequest<ReadKeyValueModel> request);

        /// <summary>
        /// Deletes the key.
        /// </summary>
        /// <param name="request">The request with key options.</param>
        /// <returns>Either true or false, indicating whether the delete succeeded.</returns>
        Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<DeleteKeyValueModel> request);
    }
}