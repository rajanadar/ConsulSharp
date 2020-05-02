using System.Collections.Generic;
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

        public Task<ConsulResponse> DeregisterAsync(ConsulRequest<string> request)
        {
            throw new System.NotImplementedException();
        }

        public Task<ConsulResponse<ServiceConfigModel>> GetConfigAsync(ConsulRequest<string> request)
        {
            throw new System.NotImplementedException();
        }

        public Task<ConsulResponse<HealthResponse>> GetHealthAsync(ConsulRequest<HealthRequest> request)
        {
            throw new System.NotImplementedException();
        }

        public Task<ConsulResponse<Dictionary<string, AgentServiceModel>>> ListAsync(ConsulRequest<string> request = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<ConsulResponse> RegisterAsync(ConsulRequest<RegisterRequest> request)
        {
            throw new System.NotImplementedException();
        }

        public Task<ConsulResponse> ToggleMaintenanceModeAsync(ConsulRequest<MaintenanceRequest> request)
        {
            throw new System.NotImplementedException();
        }
    }
}