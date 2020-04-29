using System.Collections.Generic;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Token
{
    public interface IToken
    {
        /// <summary>
        /// This endpoint creates a new ACL token.
        /// </summary>
        Task<ConsulResponse<TokenModel>> CreateAsync(ConsulRequest<CreateTokenRequest> request);

        /// <summary>
        /// This endpoint reads an ACL token with the given Accessor ID.
        /// </summary>
        Task<ConsulResponse<TokenModel>> ReadAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint returns the ACL token details that matches the secret ID 
        /// specified with the calling token.
        /// </summary>
        Task<ConsulResponse<TokenModel>> ReadSelfAsync(ConsulRequest request = null);

        /// <summary>
        /// This endpoint updates an existing ACL token.
        /// </summary>
        Task<ConsulResponse<TokenModel>> UpdateAsync(ConsulRequest<UpdateTokenRequest> request);

        /// <summary>
        /// This endpoint clones an existing ACL token.
        /// </summary>
        Task<ConsulResponse<TokenModel>> CloneAsync(ConsulRequest<CloneTokenRequest> request);

        /// <summary>
        /// This endpoint deletes an ACL token.
        /// </summary>
        Task<ConsulResponse<bool>> DeleteAsync(ConsulRequest<string> request);

        /// <summary>
        /// This endpoint lists all the ACL tokens.
        /// </summary>
        Task<ConsulResponse<List<TokenModel>>> ListAsync(ConsulRequest<ListTokensRequest> request = null);
    }
}