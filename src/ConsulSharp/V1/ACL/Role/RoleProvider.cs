using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Role
{
    internal class RoleProvider : IRole
    {
        private readonly Polymath _polymath;

        public RoleProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<RoleModel>> CreateAsync(ConsulRequest<CreateRoleRequest> request)
        {
            return await _polymath.MakeConsulApiRequest<RoleModel>(request, "v1/acl/role", HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<RoleModel>> ReadAsync(ConsulRequest<string> request)
        {
            return await _polymath.MakeConsulApiRequest<RoleModel>(request, "v1/acl/role/" + request.RequestData, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<RoleModel>> ReadByNameAsync(ConsulRequest<string> request)
        {
            return await _polymath.MakeConsulApiRequest<RoleModel>(request, "v1/acl/role/name/" + request.RequestData, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<RoleModel>> UpdateAsync(ConsulRequest<UpdateRoleRequest> request)
        {
            return await _polymath.MakeConsulApiRequest<RoleModel>(request, "v1/acl/role/" + request.RequestData.RoleId, HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<string> request)
        {
            var response = await _polymath.MakeConsulApiRequest<string>(request, "v1/acl/role/" + request.RequestData, HttpMethod.Delete, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

            return response.Map(() => bool.Parse(response.Data));
        }

        public async Task<ConsulResponse<List<RoleModel>>> ListAsync(ConsulRequest<string> request = null)
        {
            return await _polymath.MakeConsulApiRequest<List<RoleModel>>(request, "v1/acl/roles" + (!string.IsNullOrWhiteSpace(request?.RequestData) ? "?policy=" + request.RequestData : ""), HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }
}