using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
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

        public async Task<string> BootstrapAsync()
        {
            var jtoken = await _polymath.MakeConsulApiRequest<JToken>("v1/acl/bootstrap", HttpMethod.Put).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
            return jtoken["ID"].Value<string>();
        }
    }
}
