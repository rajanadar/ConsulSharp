using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.ACL.LegacyToken;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL
{
    internal class ACLProvider : IACL
    {
        private readonly Polymath _polymath;

        public ILegacyToken LegacyToken { get; }

        public ACLProvider(Polymath polymath)
        {
            _polymath = polymath;

            LegacyToken = new LegacyTokenProvider(polymath);
        }

        public async Task<ConsulResponse<BootstrapResponse>> BootstrapAsync(ConsulRequest request = null)
        {
            return await _polymath.MakeConsulApiRequest<BootstrapResponse>(request, "v1/acl/bootstrap", HttpMethod.Put).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<ReplicationStatus>> CheckReplicationAsync(ConsulRequest<string> request = null)
        {
            return await _polymath.MakeConsulApiRequest<ReplicationStatus>(request, "v1/acl/replication" + ((request != null && !string.IsNullOrWhiteSpace(request.RequestData)) ? ("?dc=" + request.RequestData) : string.Empty), HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<AuthResponse>> LoginAsync(ConsulRequest<AuthRequest> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData.AuthMethodName, nameof(request.RequestData.AuthMethodName));
            Checker.NotNull(request.RequestData.BearerToken, nameof(request.RequestData.BearerToken));

            return await _polymath.MakeConsulApiRequest<AuthResponse>(request, "v1/acl/login", HttpMethod.Post).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task LogoutAsync(ConsulRequest<string> request)
        {
            Checker.NotNull(request, nameof(request));
            Checker.NotNull(request.RequestData, nameof(request.RequestData));

            await _polymath.MakeConsulApiRequest(request, "v1/acl/logout", HttpMethod.Post, unauthenticated: true, preHeaders: new Dictionary<string, string> { { Polymath.ConsulTokenHeaderKey, request.RequestData } }).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }
}
