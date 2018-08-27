using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.Status
{
    internal class StatusProvider : IStatus
    {
        private readonly Polymath _polymath;

        public StatusProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<string>> GetRaftLeaderAsync(ConsulRequest request = null)
        {
            return await _polymath.MakeConsulApiRequest<string>(request, "v1/status/leader", HttpMethod.Get, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<List<string>>> GetRaftPeersAsync(ConsulRequest request = null)
        {
            return await _polymath.MakeConsulApiRequest<List<string>>(request, "v1/status/peers", HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }
}