using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;
using ConsulSharp.V1.Event.Models;

namespace ConsulSharp.V1.Event
{
    internal class EventProvider : IEvent
    {
        private readonly Polymath _polymath;

        public EventProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<EventModel>> FireAsync(ConsulRequest<FireEventModel> request)
        {
            return await _polymath.MakeConsulApiRequest<EventModel>(request, "v1/event/fire/" + request.RequestData.Name + request.RequestData.ToQueryString(), HttpMethod.Put, request.RequestData.RawPayload, rawRequest: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse<List<EventModel>>> ListAsync(ConsulRequest<EventFilterModel> request)
        {
            return await _polymath.MakeConsulApiRequest<List<EventModel>>(request, "v1/event/list" + request.RequestData.ToQueryString(), HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }
}