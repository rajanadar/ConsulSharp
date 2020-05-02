using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.ACL.Agent.Check;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json.Linq;

namespace ConsulSharp.V1.ACL.Agent
{
    internal class AgentProvider : IAgent
    {
        private readonly Polymath _polymath;

        public ICheck Check { get; }

        public AgentProvider(Polymath polymath)
        {
            _polymath = polymath;

            Check = new CheckProvider(polymath);
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

        public async Task<ConsulResponse> ToggleMaintenanceModeAsync(ConsulRequest<MaintenanceRequest> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData, nameof(request.RequestData));

            var qs = "?enable=" + (request.RequestData.Mode == MaintenanceMode.Enable ? "true" : "false") + (!string.IsNullOrEmpty(request.RequestData.Reason) ? "&reason=" + WebUtility.UrlEncode(request.RequestData.Reason) : string.Empty);

            return await _polymath.MakeConsulApiRequest<JToken>(request, "v1/agent/maintenance" + qs, HttpMethod.Put).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<MetricsModel>> GetMetricsAsync(ConsulRequest<MetricsRequest> request = null)
        {
            var plain = !string.IsNullOrEmpty(request?.RequestData?.Format) && string.Equals(request.RequestData.Format, MetricsFormat.PROMETHEUS, StringComparison.OrdinalIgnoreCase);

            var qs = plain ? "?format=" + MetricsFormat.PROMETHEUS : string.Empty;

            return await _polymath.MakeConsulApiRequest<MetricsModel>(request, "v1/agent/metrics" + qs, HttpMethod.Get, rawResponse: plain).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<string>> StreamLogAsync(ConsulRequest<StreamLogRequest> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData, nameof(request.RequestData));
            Checker.NotNull(request.RequestData.LogLevel, nameof(request.RequestData.LogLevel));

            var qs = "?loglevel=" + request.RequestData.LogLevel + "&logjson=" + request.RequestData.LogJson.ToString().ToLowerInvariant();

            return await _polymath.MakeConsulApiRequest<string>(request, "v1/agent/monitor" + qs, HttpMethod.Get, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse> JoinAsync(ConsulRequest<JoinRequest> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData, nameof(request.RequestData));
            Checker.NotNull(request.RequestData.AgentAddress, nameof(request.RequestData.AgentAddress));

            var qs = "?wan=" + request.RequestData.OverWAN.ToString().ToLowerInvariant();

            return await _polymath.MakeConsulApiRequest<JToken>(request, "v1/agent/join/" + request.RequestData.AgentAddress + qs, HttpMethod.Put).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse> LeaveAsync(ConsulRequest request = null)
        {
            return await _polymath.MakeConsulApiRequest<JToken>(request, "v1/agent/leave", HttpMethod.Put).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse> ForceLeaveAsync(ConsulRequest<ForceLeaveRequest> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData, nameof(request.RequestData));
            Checker.NotNull(request.RequestData.NodeName, nameof(request.RequestData.NodeName));

            var qs = request.RequestData.Prune ? "?prune" : string.Empty;

            return await _polymath.MakeConsulApiRequest<JToken>(request, "v1/agent/force-leave/" + request.RequestData.NodeName + qs, HttpMethod.Put).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse> UpdateACLTokenAsync(ConsulRequest<UpdateAgentTokenRequest> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData, nameof(request.RequestData));
            Checker.NotNull(request.RequestData.TokenType, nameof(request.RequestData.TokenType));
            Checker.NotNull(request.RequestData.Token, nameof(request.RequestData.Token));

            return await _polymath.MakeConsulApiRequest<JToken>(request, "v1/agent/token/" + request.RequestData.TokenType, HttpMethod.Put, new { Token = request.RequestData.Token }).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
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