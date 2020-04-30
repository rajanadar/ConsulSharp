using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Role
{
    public interface IRole
    {
        /// <summary>
        /// This endpoint creates a new ACL role.
        /// </summary>
        Task<ConsulResponse<RoleModel>> CreateAsync(ConsulRequest<CreateRoleRequest> request);

        /// <summary>
        /// This endpoint reads an ACL role with the given ID.
        /// </summary>
        Task<ConsulResponse<RoleModel>> ReadAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint reads an ACL role with the given name.
        /// </summary>
        Task<ConsulResponse<RoleModel>> ReadByNameAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint updates an existing ACL role.
        /// </summary>
        Task<ConsulResponse<RoleModel>> UpdateAsync(ConsulRequest<UpdateRoleRequest> request);

        /// <summary>
        /// This endpoint deletes an ACL role.
        /// </summary>
        Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint lists all the ACL tokens.
        /// </summary>
        Task<ConsulResponse<List<RoleModel>>> ListAsync(ConsulRequest<string> request = null);
    }
}