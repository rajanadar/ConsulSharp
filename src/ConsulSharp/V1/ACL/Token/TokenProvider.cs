using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Token
{
    internal class TokenProvider : IToken
    {
        private readonly Polymath _polymath;

        public TokenProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<TokenModel>> CreateAsync(ConsulRequest<CreateTokenRequest> request)
        {
            return await _polymath.MakeConsulApiRequest<TokenModel>(request, "v1/acl/token", HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<TokenModel>> ReadAsync(ConsulRequest<string> request)
        {
            return await _polymath.MakeConsulApiRequest<TokenModel>(request, "v1/acl/token/" + request.RequestData, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<TokenModel>> ReadSelfAsync(ConsulRequest request = null)
        {
            return await _polymath.MakeConsulApiRequest<TokenModel>(request, "v1/acl/token/self", HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<TokenModel>> UpdateAsync(ConsulRequest<UpdateTokenRequest> request)
        {
            return await _polymath.MakeConsulApiRequest<TokenModel>(request, "v1/acl/token/" + request.RequestData.AccessorId, HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<TokenModel>> CloneAsync(ConsulRequest<CloneTokenRequest> request)
        {
            return await _polymath.MakeConsulApiRequest<TokenModel>(request, "v1/acl/token/" + request.RequestData.AccessorId + "/clone", HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<string> request)
        {
            var response = await _polymath.MakeConsulApiRequest<string>(request, "v1/acl/token/" + request.RequestData, HttpMethod.Delete, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

            return response.Map(() => bool.Parse(response.Data));
        }

        public async Task<ConsulResponse<List<TokenModel>>> ListAsync(ConsulRequest<ListTokensRequest> request = null)
        {
            return await _polymath.MakeConsulApiRequest<List<TokenModel>>(request, "v1/acl/tokens" + GetQueryString(request.RequestData), HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        private string GetQueryString(ListTokensRequest requestData)
        {
            var result = new List<string>();

            if (requestData != null)
            {
                if (!string.IsNullOrWhiteSpace(requestData.PolicyId))
                {
                    result.Add("policy=" + requestData.PolicyId);
                }

                if (!string.IsNullOrWhiteSpace(requestData.RoleId))
                {
                    result.Add("role=" + requestData.RoleId);
                }

                if (!string.IsNullOrWhiteSpace(requestData.AuthMethod))
                {
                    result.Add("authmethod=" + requestData.AuthMethod);
                }

                if (!string.IsNullOrWhiteSpace(requestData.AuthMethodNamespace))
                {
                    result.Add("authmethod-ns=" + requestData.AuthMethodNamespace);
                }
            }

            if (result.Any())
            {
                return "?" + string.Join("&", result);
            }

            return string.Empty;
        }
    }
}