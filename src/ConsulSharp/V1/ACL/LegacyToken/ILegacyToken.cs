using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.LegacyToken
{
    public interface ILegacyToken
    {
        /// <summary>
        /// Makes a new ACL token.
        /// </summary>
        /// <param name="request">The request with token options.</param>
        /// <returns>The token id.</returns>
        Task<ConsulResponse<string>> CreateTokenAsync(ConsulRequest<TokenRequest> request);

        /// <summary>
        /// This endpoint is used to modify the policy for a given ACL token. Instead of generating a new token ID, the ID field must be provided.
        /// </summary>
        /// <param name="request">The request with token options.</param>
        /// <returns>The token id.</returns>
        Task<ConsulResponse<string>> UpdateTokenAsync(ConsulRequest<TokenRequest> request);

        /// <summary>
        /// This endpoint deletes an ACL token with the given ID.
        /// </summary>
        /// <param name="request">The request with token id.</param>
        /// <returns>A raw boolean indicating the result.</returns>
        Task<ConsulResponse<bool>> DeleteTokenAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint reads an ACL token with the given ID.
        /// </summary>
        /// <param name="request">The request with token id.</param>
        /// <returns>The token details.</returns>
        Task<ConsulResponse<List<TokenModel>>> ReadTokenAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint clones an ACL and returns a new token ID. 
        /// This allows a token to serve as a template for others, making it simple to generate new tokens without complex rule management.
        /// </summary>
        /// <param name="request">The request with token id.</param>
        /// <returns>The cloned token id.</returns>
        Task<ConsulResponse<string>> CloneTokenAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint lists all the active ACL tokens.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The list of tokens.</returns>
        Task<ConsulResponse<List<TokenModel>>> ListTokensAsync(ConsulRequest request = null);
    }
}