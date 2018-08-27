using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;
using ConsulSharp.V1.Event.Models;

namespace ConsulSharp.V1.Event
{
    /// <summary>
    /// The Event interface.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// This endpoint triggers a new user event.
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>The event model.</returns>
        Task<ConsulResponse<EventModel>> FireAsync(ConsulRequest<FireEventModel> request);

        /// <summary>
        /// This endpoint returns the most recent events (up to 256) known by the agent. 
        /// As a consequence of how the event command works, each agent may have a different view of the events. 
        /// Events are broadcast using the gossip protocol, so they have no global ordering nor do they make a promise of delivery.
        /// </summary>
        /// <param name="request">The request model.</param>
        /// <returns>The event model.</returns>
        Task<ConsulResponse<List<EventModel>>> ListAsync(ConsulRequest<EventFilterModel> request);
    }
}