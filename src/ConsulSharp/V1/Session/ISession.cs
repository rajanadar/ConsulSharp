using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;
using ConsulSharp.V1.Session.Models;

namespace ConsulSharp.V1.Session
{
    /// <summary>
    /// The Session interface.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// This endpoint initializes a new session. 
        /// Sessions must be associated with a node and may be associated with any number of checks.
        /// </summary>
        /// <param name="request">The creation options.</param>
        /// <returns>The session id.</returns>
        Task<ConsulResponse<string>> CreateAsync(ConsulRequest<SessionRequestModel> request);

        /// <summary>
        /// This endpoint destroys the session with the given name. 
        /// If the session UUID is malformed, an error is returned. 
        /// If the session UUID does not exist or already expired, a success is still returned (the operation is idempotent).
        /// </summary>
        /// <param name="request">The request with session options.</param>
        /// <returns>Either true or false, indicating whether the operation succeeded.</returns>
        Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<SessionDeleteModel> request);

        /// <summary>
        /// This endpoint returns the requested session information.
        /// </summary>
        /// <param name="request">The request with session options.</param>
        /// <returns>A list of sessions.</returns>
        Task<ConsulResponse<List<SessionModel>>> ReadAsync(ConsulRequest<SessionReadModel> request);

        /// <summary>
        /// This endpoint returns the active sessions for a given node.
        /// </summary>
        /// <param name="request">The request with node options.</param>
        /// <returns>A list of sessions.</returns>
        Task<ConsulResponse<List<SessionModel>>> ReadNodeSessionsAsync(ConsulRequest<NodeSessionReadModel> request);

        /// <summary>
        /// This endpoint returns the list of active sessions.
        /// </summary>
        /// <param name="request">The request with session options.</param>
        /// <returns>A list of sessions.</returns>
        Task<ConsulResponse<List<SessionModel>>> ListAsync(ConsulRequest<string> request = null);

        /// <summary>
        /// This endpoint renews the given session. 
        /// This is used with sessions that have a TTL, and it extends the expiration by the TTL.
        /// Consul may return a TTL value higher than the one specified during session creation. 
        /// This indicates the server is under high load and is requesting clients renew less often.
        /// </summary>
        /// <param name="request">The request with session id.</param>
        /// <returns>The renewed info.</returns>
        Task<ConsulResponse<List<SessionModel>>> RenewAsync(ConsulRequest<RenewSessionRequestModel> request);
    }
}