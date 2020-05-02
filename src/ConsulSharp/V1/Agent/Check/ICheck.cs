using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Agent.Check
{
    public interface ICheck
    {
        Task<ConsulResponse<Dictionary<string, CheckModel>>> ListAsync(ConsulRequest<string> request = null);

        Task<ConsulResponse<string>> RegisterAsync(ConsulRequest<CheckRequest> request);

        Task<ConsulResponse> DeregisterAsync(ConsulRequest<string> request);
    }
}