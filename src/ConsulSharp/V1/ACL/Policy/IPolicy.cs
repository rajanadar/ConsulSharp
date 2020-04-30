using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Policy
{
    public interface IPolicy
    {
        /// <summary>
        /// This endpoint creates a new ACL policy.
        /// </summary>
        Task<ConsulResponse<PolicyModel>> CreateAsync(ConsulRequest<CreatePolicyRequest> request);

        /// <summary>
        /// This endpoint reads an ACL policy with the given ID.
        /// </summary>
        Task<ConsulResponse<PolicyModel>> ReadAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint updates an existing ACL policy.
        /// </summary>
        Task<ConsulResponse<PolicyModel>> UpdateAsync(ConsulRequest<UpdatePolicyRequest> request);

        /// <summary>
        /// This endpoint deletes an ACL policy.
        /// </summary>
        Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint lists all the ACL tokens.
        /// </summary>
        Task<ConsulResponse<List<PolicyModel>>> ListAsync(ConsulRequest request = null);
    }
}