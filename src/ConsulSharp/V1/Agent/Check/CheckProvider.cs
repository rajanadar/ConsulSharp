using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json.Linq;

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

        public async Task<ConsulResponse<string>> RegisterAsync(ConsulRequest<CheckRequest> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData, nameof(request.RequestData));

            return await _polymath.MakeConsulApiRequest<string>(request, "v1/agent/check/register", HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse> DeregisterAsync(ConsulRequest<string> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData, nameof(request.RequestData));

            return await _polymath.MakeConsulApiRequest<JToken>(request, "v1/agent/check/deregister/" + request.RequestData, HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse> SetTTLTypeStatusAsync(ConsulRequest<StatusRequest> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData, nameof(request.RequestData));

            return await _polymath.MakeConsulApiRequest<JToken>(request, "v1/agent/check/update/" + request.RequestData.CheckId, HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }
}