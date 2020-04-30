﻿using System.Collections.Generic;
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
    }
}