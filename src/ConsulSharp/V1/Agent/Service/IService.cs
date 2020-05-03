using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Agent.Service
{
    public interface IService
    {
        Task<ConsulResponse<Dictionary<string, AgentServiceModel>>> ListAsync(ConsulRequest<string> request = null);

        Task<ConsulResponse<AgentServiceModel>> GetConfigAsync(ConsulRequest<string> request);

        Task<ConsulResponse<HealthResponse>> GetHealthAsync(ConsulRequest<HealthRequest> request);

        Task<ConsulResponse> RegisterAsync(ConsulRequest<RegisterRequest> request);

        Task<ConsulResponse> DeregisterAsync(ConsulRequest<string> request);

        Task<ConsulResponse> ToggleMaintenanceModeAsync(ConsulRequest<MaintenanceRequest> request);
    }
}