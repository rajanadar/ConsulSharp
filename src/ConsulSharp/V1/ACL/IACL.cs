using System;
using System.Threading.Tasks;
using ConsulSharp.V1.ACL.LegacyToken;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL
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
        Task<ConsulResponse<BootstrapResponse>> BootstrapAsync(ConsulRequest request = null);

        /// <summary>
        /// This endpoint returns the status of the ACL replication process in the datacenter. 
        /// This is intended to be used by operators, or by automation checking the health of ACL replication.
        /// </summary>
        /// <param name="request">
        /// The request with the datacenter to query. 
        /// This will default to the datacenter of the agent being queried. 
        /// This is specified as part of the URL as a query parameter.
        /// </param>
        /// <returns>The replication details.</returns>
        Task<ConsulResponse<ReplicationStatus>> CheckReplicationAsync(ConsulRequest<string> request = null);

        /// <summary>
        /// This endpoint was added in Consul 1.5.0 and is used to exchange an auth method bearer 
        /// token for a newly-created Consul ACL token.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>The response.</returns>
        Task<ConsulResponse<AuthResponse>> LoginAsync(ConsulRequest<AuthRequest> request);

        /// <summary>
        /// This endpoint was added in Consul 1.5.0 and is used to destroy a token created via the login endpoint. 
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>The response.</returns>
        Task LogoutAsync(ConsulRequest<string> request);

        /// <summary>
        /// Consul 1.4.0 deprecates the legacy ACL system completely. 
        /// It's strongly recommended you do not build anything using the legacy system 
        /// and consider using the new ACL Token and Policy APIs instead.
        /// </summary>
        [Obsolete]
        ILegacyToken LegacyToken { get; }
    }
}
