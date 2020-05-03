using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.ACL.Agent.Service;
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

            if (plain)
            {
                var plainText = await _polymath.MakeConsulApiRequest<string>(request, "v1/agent/health/service/" + qs, HttpMethod.Get, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

                return plainText.Map(() => new HealthResponse { PlainText = plainText.Data });
            }

            if (!string.IsNullOrEmpty(request.RequestData.ServiceId))
            {
                var service = await _polymath.MakeConsulApiRequest<Dictionary<string, AgentServiceModel>>(request, "v1/agent/health/service/" + qs, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

                return service.Map(() => new HealthResponse { Statuses = service.Data.ToDictionary(kv => kv.Key, kv => new List<AgentServiceModel> { kv.Value }) });
            }

            var services = await _polymath.MakeConsulApiRequest<Dictionary<string, List<AgentServiceModel>>>(request, "v1/agent/health/service/" + qs, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

            return services.Map(() => new HealthResponse { Statuses = services.Data });
        }

        public Task<ConsulResponse> RegisterAsync(ConsulRequest<RegisterRequest> request)
        {
            throw new System.NotImplementedException();
        }

        public Task<ConsulResponse> DeregisterAsync(ConsulRequest<string> request)
        {
            throw new System.NotImplementedException();
        }

        public Task<ConsulResponse> ToggleMaintenanceModeAsync(ConsulRequest<MaintenanceRequest> request)
        {
            throw new System.NotImplementedException();
        }
    }
}