using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Agent
{
    public interface IAgent
    {
        /// <summary>
        /// This endpoint returns the members the agent sees in the cluster gossip pool. 
        /// Due to the nature of gossip, this is eventually consistent: the results may differ by agent. 
        /// </summary>
        Task<ConsulResponse<List<MemberModel>>> ListMembersAsync(ConsulRequest<ListMemberRequest> request = null);

        /// <summary>
        /// This endpoint returns the configuration and member information of the local agent. 
        /// The Config element contains a subset of the configuration and its format will not change 
        /// in a backwards incompatible way between releases. 
        /// DebugConfig contains the full runtime configuration but its format 
        /// is subject to change without notice or deprecation.
        /// </summary>
        Task<ConsulResponse<ConfigAndMemberModel>> ReadConfigAsync(ConsulRequest request = null);

        /// <summary>
        /// This endpoint instructs the agent to reload its configuration. 
        /// Any errors encountered during this process are returned.
        /// </summary>
        Task<ConsulResponse<Dictionary<string, object>>> ReloadConfigAsync(ConsulRequest request = null);

        /// <summary>
        /// This endpoint places the agent into "maintenance mode". 
        /// During maintenance mode, the node will be marked as unavailable and will not 
        /// be present in DNS or API queries. This API call is idempotent.
        /// Maintenance mode is persistent and will be automatically restored on agent restart.
        /// </summary>
        Task<ConsulResponse> ToggleMaintenanceModeAsync(ConsulRequest<MaintenanceRequest> request);

        /// <summary>
        /// This endpoint will dump the metrics for the most recent finished interval. 
        /// </summary>
        Task<ConsulResponse<MetricsModel>> GetMetricsAsync(ConsulRequest<MetricsRequest> request = null);

        // Task<ConsulResponse<string>> StreamLogAsync(ConsulRequest<StreamLogRequest> request);

        /// <summary>
        /// This endpoint instructs the agent to attempt to connect to a given address.
        /// </summary>
        Task<ConsulResponse> JoinAsync(ConsulRequest<JoinRequest> request);
    }
}