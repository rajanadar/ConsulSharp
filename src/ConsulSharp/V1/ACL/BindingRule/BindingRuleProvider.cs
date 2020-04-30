using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.BindingRule
{
    internal class BindingRuleProvider : IBindingRule
    {
        private readonly Polymath _polymath;

        public BindingRuleProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<BindingRuleModel>> CreateAsync(ConsulRequest<CreateBindingRuleRequest> request)
        {
            return await _polymath.MakeConsulApiRequest<BindingRuleModel>(request, "v1/acl/binding-rule", HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<BindingRuleModel>> ReadAsync(ConsulRequest<string> request)
        {
            return await _polymath.MakeConsulApiRequest<BindingRuleModel>(request, "v1/acl/binding-rule/" + request.RequestData, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<BindingRuleModel>> UpdateAsync(ConsulRequest<UpdateBindingRuleRequest> request)
        {
            return await _polymath.MakeConsulApiRequest<BindingRuleModel>(request, "v1/acl/binding-rule/" + request.RequestData.BindingRuleId, HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<string> request)
        {
            var response = await _polymath.MakeConsulApiRequest<string>(request, "v1/acl/binding-rule/" + request.RequestData, HttpMethod.Delete, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

            return response.Map(() => bool.Parse(response.Data));
        }

        public async Task<ConsulResponse<List<BindingRuleModel>>> ListAsync(ConsulRequest<string> request = null)
        {
            return await _polymath.MakeConsulApiRequest<List<BindingRuleModel>>(request, "v1/acl/binding-rules" + (!string.IsNullOrWhiteSpace(request?.RequestData) ? "?authmethod=" + request.RequestData : ""), HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }
}