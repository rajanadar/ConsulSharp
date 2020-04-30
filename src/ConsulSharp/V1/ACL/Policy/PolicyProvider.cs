using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Policy
{
    internal class PolicyProvider : IPolicy
    {
        private readonly Polymath _polymath;

        public PolicyProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<PolicyModel>> CreateAsync(ConsulRequest<CreatePolicyRequest> request)
        {
            return await _polymath.MakeConsulApiRequest<PolicyModel>(request, "v1/acl/policy", HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<PolicyModel>> ReadAsync(ConsulRequest<string> request)
        {
            return await _polymath.MakeConsulApiRequest<PolicyModel>(request, "v1/acl/policy/" + request.RequestData, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<PolicyModel>> UpdateAsync(ConsulRequest<UpdatePolicyRequest> request)
        {
            return await _polymath.MakeConsulApiRequest<PolicyModel>(request, "v1/acl/policy/" + request.RequestData.PolicyId, HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<string> request)
        {
            var response = await _polymath.MakeConsulApiRequest<string>(request, "v1/acl/policy/" + request.RequestData, HttpMethod.Delete, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

            return response.Map(() => bool.Parse(response.Data));
        }

        public async Task<ConsulResponse<List<PolicyModel>>> ListAsync(ConsulRequest request = null)
        {
            return await _polymath.MakeConsulApiRequest<List<PolicyModel>>(request, "v1/acl/policies", HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }
}