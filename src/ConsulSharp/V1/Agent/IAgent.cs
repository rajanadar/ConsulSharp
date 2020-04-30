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
        Task<ConsulResponse<List<MemberModel>>> ListMembersAsync(ConsulRequest<ListMemberRequest> request);
    }
}