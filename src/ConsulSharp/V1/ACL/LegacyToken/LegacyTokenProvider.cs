using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json.Linq;

namespace ConsulSharp.V1.ACL.LegacyToken
{
    internal class LegacyTokenProvider : ILegacyToken
    {
        private readonly Polymath _polymath;

        public LegacyTokenProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<string>> CreateAsync(ConsulRequest<TokenRequest> request)
        {
            var jtokenResponse = await _polymath.MakeConsulApiRequest<JToken>(request, "v1/acl/create", HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
            return jtokenResponse.Map(() => jtokenResponse.Data["ID"].Value<string>());
        }

        public async Task<ConsulResponse<string>> UpdateAsync(ConsulRequest<TokenRequest> request)
        {
            var jtokenResponse = await _polymath.MakeConsulApiRequest<JToken>(request, "v1/acl/update", HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
            return jtokenResponse.Map(() => jtokenResponse.Data["ID"].Value<string>());
        }

        public async Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<string> request)
        {
            var response = await _polymath.MakeConsulApiRequest<string>(request, "v1/acl/destroy/" + request.RequestData, HttpMethod.Put, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
            return response.Map(() => bool.Parse(response.Data));
        }

        public async Task<ConsulResponse<List<TokenModel>>> ReadAsync(ConsulRequest<string> request)
        {
            return await _polymath.MakeConsulApiRequest<List<TokenModel>>(request, "v1/acl/info/" + request.RequestData, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<string>> CloneAsync(ConsulRequest<string> request)
        {
            var jtokenResponse = await _polymath.MakeConsulApiRequest<JToken>(request, "v1/acl/clone/" + request.RequestData, HttpMethod.Put).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
            return jtokenResponse.Map(() => jtokenResponse.Data["ID"].Value<string>());
        }

        public async Task<ConsulResponse<List<TokenModel>>> ListAsync(ConsulRequest request = null)
        {
            return await _polymath.MakeConsulApiRequest<List<TokenModel>>(request, "v1/acl/list", HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }
}