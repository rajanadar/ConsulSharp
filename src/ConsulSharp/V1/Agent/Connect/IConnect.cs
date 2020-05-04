using System.Threading.Tasks;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Agent.Connect
{
    public interface IConnect
    {
        /// <summary>
        /// This endpoint tests whether a connection attempt is authorized between two services. 
        /// This is the primary API that must be implemented by proxies or native integrations that wish 
        /// to integrate with Connect. Prior to calling this API, it is expected that the 
        /// client TLS certificate has been properly verified against the current CA roots.
        /// The implementation of this API uses locally cached data and doesn't require any request 
        /// forwarding to a server. 
        /// Therefore, the response typically occurs in microseconds to impose minimal overhead on the connection attempt.
        /// </summary>
        Task<ConsulResponse<ConnectAuthResponse>> AuthorizeAsync(ConsulRequest<ConnectAuthRequest> request);

        /// <summary>
        /// This endpoint returns the trusted certificate authority (CA) root certificates. 
        /// This is used by proxies or native integrations to verify served client or server certificates are valid.
        /// This is equivalent to the non-Agent Connect endpoint, 
        /// but the response of this request is cached locally at the agent.
        /// This allows for very fast response times and for fail open behavior if the server is unavailable.
        /// This endpoint should be used by proxies and native integrations.
        /// </summary>
        Task<ConsulResponse<CARootsResponse>> GetCARootsAsync(ConsulRequest request = null);

        /// <summary>
        /// This endpoint returns the leaf certificate representing a single service. 
        /// This certificate is used as a server certificate for accepting inbound connections 
        /// and is also used as the client certificate for establishing outbound connections to other services.
        /// The agent generates a CSR locally and calls the CA sign API to sign it.
        /// The resulting certificate is cached and returned by this API until it is 
        /// near expiry or the root certificates change.
        /// This API supports blocking queries.
        /// The blocking query will block until a new certificate is necessary 
        /// because the existing certificate will expire or the root certificate is being rotated.
        /// This blocking behavior allows clients to efficiently wait for certificate rotations.
        /// </summary>
        Task<ConsulResponse<CertificateModel>> GetLeafCertificateAsync(ConsulRequest<string> request);
    }
}