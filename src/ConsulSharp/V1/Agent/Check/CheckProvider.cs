using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Agent.Check
{
    internal class CheckProvider : ICheck
    {
        private readonly Polymath _polymath;

        public CheckProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<Dictionary<string, CheckModel>>> ListAsync(ConsulRequest<string> request = null)
        {
            var qs = string.IsNullOrWhiteSpace(request?.RequestData) ? string.Empty : ("?filter=" + request.RequestData);

            return await _polymath.MakeConsulApiRequest<Dictionary<string, CheckModel>>(request, "v1/agent/checks" + qs, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }
}