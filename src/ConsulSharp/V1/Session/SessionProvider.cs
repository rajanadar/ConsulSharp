using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;
using ConsulSharp.V1.Session.Models;
using Newtonsoft.Json.Linq;

namespace ConsulSharp.V1.Session
{
    internal class SessionProvider : ISession
    {
        private readonly Polymath _polymath;

        public SessionProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<string>> CreateAsync(ConsulRequest<SessionRequestModel> request)
        {
            var jtokenResponse = await _polymath.MakeConsulApiRequest<JToken>(request, "v1/session/create" + request.RequestData.ToQueryString(), HttpMethod.Put, request.RequestData).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
            return jtokenResponse.Map(() => jtokenResponse.Data["ID"].Value<string>());
        }

        public async Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<SessionDeleteModel> request)
        {
            var response = await _polymath.MakeConsulApiRequest<string>(request, "v1/session/destroy/" + request.RequestData.SessionId + request.RequestData.ToQueryString(), HttpMethod.Put, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
            return response.Map(() => bool.Parse(response.Data));
        }

        public async Task<ConsulResponse<List<SessionModel>>> ReadAsync(ConsulRequest<SessionReadModel> request)
        {
            return await _polymath.MakeConsulApiRequest<List<SessionModel>>(request, "v1/session/info/" + request.RequestData.SessionId + request.RequestData.ToQueryString(), HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<List<SessionModel>>> ReadNodeSessionsAsync(ConsulRequest<NodeSessionReadModel> request)
        {
            return await _polymath.MakeConsulApiRequest<List<SessionModel>>(request, "v1/session/node/" + request.RequestData.Node + request.RequestData.ToQueryString(), HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<List<SessionModel>>> ListAsync(ConsulRequest<string> request = null)
        {
            return await _polymath.MakeConsulApiRequest<List<SessionModel>>(request, "v1/session/list" + ((request != null && !string.IsNullOrWhiteSpace(request.RequestData)) ? "?dc=" + request.RequestData : string.Empty), HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<List<SessionModel>>> RenewAsync(ConsulRequest<RenewSessionRequestModel> request)
        {
            return await _polymath.MakeConsulApiRequest<List<SessionModel>>(request, "v1/session/renew/" + request.RequestData.SessionId + request.RequestData.ToQueryString(), HttpMethod.Put).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }
}