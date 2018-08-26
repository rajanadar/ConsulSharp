using System.Threading.Tasks;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1
{
    /// <summary>
    /// The ACL interface.
    /// </summary>
    public interface IACL
    {
        /// <summary>
        /// This endpoint does a special one-time bootstrap of the ACL system, making the first management token 
        /// if the acl_master_token is not specified in the Consul server configuration, 
        /// and if the cluster has not been bootstrapped previously. 
        /// This is available in Consul 0.9.1 and later, and requires all Consul servers to be upgraded in 
        /// order to operate.
        /// This provides a mechanism to bootstrap ACLs without having any secrets present in Consul's configuration files.
        /// </summary>
        /// <returns>The Id of the management token.</returns>
        Task<ConsulResponse<string>> BootstrapAsync(ConsulRequest request = null);

        /// <summary>
        /// Makes a new ACL token.
        /// </summary>
        /// <param name="request">The request with token options.</param>
        /// <returns>The token id.</returns>
        Task<ConsulResponse<string>> CreateTokenAsync(ConsulRequest<TokenRequestModel> request);
    }
}
