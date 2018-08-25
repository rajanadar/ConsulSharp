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

        public async Task<Response<string>> BootstrapAsync(Request request = null)
        {
            var jtokenResponse = await _polymath.MakeConsulApiRequest<JToken>(request, "v1/acl/bootstrap", HttpMethod.Put).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

            return new Response<string>
            {
                Index = jtokenResponse.Index,
                ResponseData = jtokenResponse.ResponseData["ID"].Value<string>(),
            };
        }
    }
}
