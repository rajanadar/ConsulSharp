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

        // raja todo> write this api in a stream oriented cancelation token base puppy format.
        // Task<ConsulResponse<string>> StreamLogAsync(ConsulRequest<StreamLogRequest> request);

        /// <summary>
        /// This endpoint instructs the agent to attempt to connect to a given address.
        /// </summary>
        Task<ConsulResponse> JoinAsync(ConsulRequest<JoinRequest> request);

        /// <summary>
        /// This endpoint triggers a graceful leave and shutdown of the agent. 
        /// It is used to ensure other nodes see the agent as "left" instead of "failed". 
        /// Nodes that leave will not attempt to re-join the cluster on restarting with a snapshot.
        /// For nodes in server mode, the node is removed from the Raft peer set in a graceful manner.
        /// This is critical, as in certain situations a non-graceful leave can affect cluster availability.
        /// </summary>
        Task<ConsulResponse> LeaveAsync(ConsulRequest request = null);

        /// <summary>
        /// This endpoint instructs the agent to force a node into the left state. 
        /// If a node fails unexpectedly, then it will be in a failed state. 
        /// Once in the failed state, Consul will attempt to reconnect, and the services and checks 
        /// belonging to that node will not be cleaned up. 
        /// Forcing a node into the left state allows its old entries to be removed.
        /// </summary>
        Task<ConsulResponse> ForceLeaveAsync(ConsulRequest<ForceLeaveRequest> request);

        /// <summary>
        /// This endpoint updates the ACL tokens currently in use by the agent. 
        /// It can be used to introduce ACL tokens to the agent for the first time, 
        /// or to update tokens that were initially loaded from the agent's configuration. 
        /// Tokens will be persisted only if the acl.enable_token_persistence configuration is true. 
        /// When not being persisted, they will need to be reset if the agent is restarted.
        /// </summary>
        Task<ConsulResponse> UpdateACLTokenAsync(ConsulRequest<UpdateAgentTokenRequest> request);
    }
}