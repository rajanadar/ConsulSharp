using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Agent.Service
{
    public interface IService
    {
        /// <summary>
        /// This endpoint returns all the services that are registered with the local agent. 
        /// These services were either provided through configuration files or added dynamically using the HTTP API.
        /// It is important to note that the services known by the agent may be different from those reported by the catalog.
        /// This is usually due to changes being made while there is no leader elected.
        /// The agent performs active anti-entropy, so in most situations everything will be in sync within a few seconds.
        /// </summary>
        Task<ConsulResponse<Dictionary<string, AgentServiceModel>>> ListAsync(ConsulRequest<string> request = null);

        /// <summary>
        /// This endpoint was added in Consul 1.3.0 and returns the full service definition for a single 
        /// service instance registered on the local agent. 
        /// It is used by Connect proxies to discover the embedded proxy configuration that 
        /// was registered with the instance.
        /// It is important to note that the services known by the agent may be different 
        /// from those reported by the catalog.
        /// This is usually due to changes being made while there is no leader elected.
        /// The agent performs active anti-entropy, so in most situations everything will be in sync within a few seconds.
        /// </summary>
        Task<ConsulResponse<AgentServiceModel>> GetConfigAsync(ConsulRequest<string> request);

        /// <summary>
        /// Retrieve an aggregated state of service(s) on the local agent by name.
        /// This endpoints support JSON format and text/plain formats,
        /// </summary>
        Task<ConsulResponse<HealthResponse>> GetHealthAsync(ConsulRequest<HealthRequest> request);

        /// <summary>
        /// This endpoint adds a new service, with optional health checks, to the local agent.
        /// The agent is responsible for managing the status of its local services, 
        /// and for sending updates about its local services to the servers to keep the global catalog in sync.
        /// </summary>
        Task<ConsulResponse> RegisterAsync(ConsulRequest<RegisterRequest> request);

        /// <summary>
        /// This endpoint removes a service from the local agent. 
        /// If the service does not exist, no action is taken.
        /// The agent will take care of deregistering the service with the catalog.
        /// If there is an associated check, that is also deregistered.
        /// </summary>
        Task<ConsulResponse> DeregisterAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint places a given service into "maintenance mode". 
        /// During maintenance mode, the service will be marked as unavailable and will not be present in 
        /// DNS or API queries. 
        /// This API call is idempotent. 
        /// Maintenance mode is persistent and will be automatically restored on agent restart.
        /// </summary>
        Task<ConsulResponse> ToggleMaintenanceModeAsync(ConsulRequest<MaintenanceRequest> request);
    }
}