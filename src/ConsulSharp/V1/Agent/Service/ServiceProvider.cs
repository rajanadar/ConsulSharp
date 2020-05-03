using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json.Linq;

namespace ConsulSharp.V1.ACL.Agent.Service
{
    internal class ServiceProvider : IService
    {
        private readonly Polymath _polymath;

        public ServiceProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<Dictionary<string, AgentServiceModel>>> ListAsync(ConsulRequest<string> request = null)
        {
            var qs = string.IsNullOrEmpty(request?.RequestData) ? string.Empty : "?filter=" + request.RequestData;

            return await _polymath.MakeConsulApiRequest<Dictionary<string, AgentServiceModel>>(request, "v1/agent/services" + qs, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }


        public async Task<ConsulResponse<AgentServiceModel>> GetConfigAsync(ConsulRequest<string> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData, nameof(request.RequestData));

            return await _polymath.MakeConsulApiRequest<AgentServiceModel>(request, "v1/agent/service/" + request.RequestData, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<HealthResponse>> GetHealthAsync(ConsulRequest<HealthRequest> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData, nameof(request.RequestData));

            if (string.IsNullOrEmpty(request.RequestData.ServiceId) && string.IsNullOrEmpty(request.RequestData.ServiceName))
            {
                throw new ArgumentException("Provide one of ServiceId or ServiceName. Both cannot be missing.");
            }

            var qs = !string.IsNullOrEmpty(request.RequestData.ServiceId) ? "id/" + request.RequestData.ServiceId : "name/" + request.RequestData.ServiceName;

            var plain = !string.IsNullOrEmpty(request.RequestData.Format) && request.RequestData.Format.Equals(ContentFormat.TEXT, StringComparison.OrdinalIgnoreCase);

            qs += (plain ? "?format=text" : string.Empty);

            var status = HttpStatusCode.OK;

            if (plain)
            {
                var plainText = await _polymath.MakeConsulApiRequest<string>(request, "v1/agent/health/service/" + qs, HttpMethod.Get, rawResponse: true, postResponseFunc: r => { status = r.StatusCode; return true; }).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

                return plainText.Map(() => new HealthResponse { StatusCode = status, PlainText = plainText.Data });
            }

            if (!string.IsNullOrEmpty(request.RequestData.ServiceId))
            {
                var service = await _polymath.MakeConsulApiRequest<Dictionary<string, AgentServiceModel>>(request, "v1/agent/health/service/" + qs, HttpMethod.Get, postResponseFunc: r => { status = r.StatusCode; return true; }).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

                return service.Map(() => new HealthResponse { StatusCode = status, Statuses = service.Data.ToDictionary(kv => kv.Key, kv => new List<AgentServiceModel> { kv.Value }) });
            }

            var services = await _polymath.MakeConsulApiRequest<Dictionary<string, List<AgentServiceModel>>>(request, "v1/agent/health/service/" + qs, HttpMethod.Get, postResponseFunc: r => { status = r.StatusCode; return true; }).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

            return services.Map(() => new HealthResponse { StatusCode = status, Statuses = services.Data });
        }

        public async Task<ConsulResponse> RegisterAsync(ConsulRequest<RegisterRequest> request)
        {
            return await _polymath.MakeConsulApiRequest<JToken>(request, "v1/agent/service/register", HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse> DeregisterAsync(ConsulRequest<string> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData, nameof(request.RequestData));

            return await _polymath.MakeConsulApiRequest<JToken>(request, "v1/agent/service/deregister/" + request.RequestData, HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse> ToggleMaintenanceModeAsync(ConsulRequest<MaintenanceRequest> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData, nameof(request.RequestData));

            var qs = "?enable=" + (request.RequestData.Mode == MaintenanceMode.Enable ? "true" : "false") + (!string.IsNullOrEmpty(request.RequestData.Reason) ? "&reason=" + WebUtility.UrlEncode(request.RequestData.Reason) : string.Empty);

            return await _polymath.MakeConsulApiRequest<JToken>(request, "v1/agent/service/maintenance/" + request.RequestData.ServiceId + qs, HttpMethod.Put).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }
}