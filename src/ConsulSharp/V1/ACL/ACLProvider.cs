using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json.Linq;

namespace ConsulSharp.V1
{
    internal class ACLProvider : IACL
    {
        private readonly Polymath _polymath;

        public ACLProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<string>> BootstrapAsync(ConsulRequest request = null)
        {
            var jtokenResponse = await _polymath.MakeConsulApiRequest<JToken>(request, "v1/acl/bootstrap", HttpMethod.Put).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
            return jtokenResponse.Map(() => jtokenResponse.ResponseData["ID"].Value<string>());
        }

        public async Task<ConsulResponse<string>> CreateTokenAsync(ConsulRequest<TokenRequestModel> request)
        {
            var jtokenResponse = await _polymath.MakeConsulApiRequest<JToken>(request, "v1/acl/create", HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
            return jtokenResponse.Map(() => jtokenResponse.ResponseData["ID"].Value<string>());
        }
    }
}
