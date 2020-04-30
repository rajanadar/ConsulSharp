using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.AuthMethod
{
    internal class AuthMethodProvider : IAuthMethod
    {
        private readonly Polymath _polymath;

        public AuthMethodProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<AuthMethodModel>> CreateAsync(ConsulRequest<CreateAuthMethodRequest> request)
        {
            return await _polymath.MakeConsulApiRequest<AuthMethodModel>(request, "v1/acl/auth-method", HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<AuthMethodModel>> ReadAsync(ConsulRequest<string> request)
        {
            return await _polymath.MakeConsulApiRequest<AuthMethodModel>(request, "v1/acl/auth-method/" + request.RequestData, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<AuthMethodModel>> UpdateAsync(ConsulRequest<UpdateAuthMethodRequest> request)
        {
            return await _polymath.MakeConsulApiRequest<AuthMethodModel>(request, "v1/acl/auth-method/" + request.RequestData.Name, HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<string> request)
        {
            var response = await _polymath.MakeConsulApiRequest<string>(request, "v1/acl/auth-method/" + request.RequestData, HttpMethod.Delete, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

            return response.Map(() => bool.Parse(response.Data));
        }

        public async Task<ConsulResponse<List<AuthMethodModel>>> ListAsync(ConsulRequest<string> request = null)
        {
            return await _polymath.MakeConsulApiRequest<List<AuthMethodModel>>(request, "v1/acl/auth-methods" + (!string.IsNullOrWhiteSpace(request?.RequestData) ? "?policy=" + request.RequestData : ""), HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }
}