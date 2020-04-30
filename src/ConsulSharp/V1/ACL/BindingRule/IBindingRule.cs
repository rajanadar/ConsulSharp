using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.BindingRule
{
    public interface IBindingRule
    {
        /// <summary>
        /// This endpoint creates a new ACL bindingRule.
        /// </summary>
        Task<ConsulResponse<BindingRuleModel>> CreateAsync(ConsulRequest<CreateBindingRuleRequest> request);

        /// <summary>
        /// This endpoint reads an ACL bindingRule with the given ID.
        /// </summary>
        Task<ConsulResponse<BindingRuleModel>> ReadAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint updates an existing ACL bindingRule.
        /// </summary>
        Task<ConsulResponse<BindingRuleModel>> UpdateAsync(ConsulRequest<UpdateBindingRuleRequest> request);

        /// <summary>
        /// This endpoint deletes an ACL bindingRule.
        /// </summary>
        Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint lists all the ACL tokens.
        /// </summary>
        Task<ConsulResponse<List<BindingRuleModel>>> ListAsync(ConsulRequest<string> request = null);
    }
}