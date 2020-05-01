using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Agent
{
    internal class AgentProvider : IAgent
    {
        private readonly Polymath _polymath;

        public AgentProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<List<MemberModel>>> ListMembersAsync(ConsulRequest<ListMemberRequest> request = null)
        {
            return await _polymath.MakeConsulApiRequest<List<MemberModel>>(request, "v1/agent/members" + GetQueryString(request.RequestData), HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<ConfigAndMemberModel>> ReadConfigAsync(ConsulRequest request = null)
        {
            return await _polymath.MakeConsulApiRequest<ConfigAndMemberModel>(request, "v1/agent/self", HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<Dictionary<string, object>>> ReloadConfigAsync(ConsulRequest request = null)
        {
            return await _polymath.MakeConsulApiRequest<Dictionary<string, object>>(request, "v1/agent/reload", HttpMethod.Put).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        private string GetQueryString(ListMemberRequest requestData)
        {
            var result = new List<string>();

            if (requestData != null)
            {
                if (requestData.WANMembers == true)
                {
                    result.Add("wan=true");
                }

                if (!string.IsNullOrWhiteSpace(requestData.Segment))
                {
                    result.Add("segment=" + requestData.Segment);
                }
            }

            if (result.Any())
            {
                return "?" + string.Join("&", result);
            }

            return string.Empty;
        }
    }
}