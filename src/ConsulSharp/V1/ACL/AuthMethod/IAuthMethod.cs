using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.AuthMethod
{
    public interface IAuthMethod
    {
        /// <summary>
        /// This endpoint creates a new ACL authMethod.
        /// </summary>
        Task<ConsulResponse<AuthMethodModel>> CreateAsync(ConsulRequest<CreateAuthMethodRequest> request);

        /// <summary>
        /// This endpoint reads an ACL authMethod with the given ID.
        /// </summary>
        Task<ConsulResponse<AuthMethodModel>> ReadAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint updates an existing ACL authMethod.
        /// </summary>
        Task<ConsulResponse<AuthMethodModel>> UpdateAsync(ConsulRequest<UpdateAuthMethodRequest> request);

        /// <summary>
        /// This endpoint deletes an ACL authMethod.
        /// </summary>
        Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint lists all the ACL tokens.
        /// </summary>
        Task<ConsulResponse<List<AuthMethodModel>>> ListAsync(ConsulRequest<string> request = null);
    }
}