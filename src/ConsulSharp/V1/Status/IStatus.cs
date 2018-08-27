using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.Status
{
    /// <summary>
    /// The Status interface.
    /// </summary>
    public interface IStatus
    {
        /// <summary>
        /// This endpoint returns the Raft leader for the datacenter in which the agent is running.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The leader IP Address with port.</returns>
        Task<ConsulResponse<string>> GetRaftLeaderAsync(ConsulRequest request = null);

        /// <summary>
        /// This endpoint retrieves the Raft peers for the datacenter in which the the agent is running. 
        /// This list of peers is strongly consistent and can be useful in determining when a given server has successfully joined the cluster.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The raft peers.</returns>
        Task<ConsulResponse<List<string>>> GetRaftPeersAsync(ConsulRequest request = null);
    }
}