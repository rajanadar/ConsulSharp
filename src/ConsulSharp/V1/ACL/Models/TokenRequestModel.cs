namespace ConsulSharp.V1.ACL.Models
{
    /// <summary>
    /// The Token Request model.
    /// </summary>
    public class TokenRequestModel : AbstractTokenModel
    {
        /// <summary>
        /// Initializes an instance of <see cref="TokenRequestModel"/>.
        /// </summary>
        public TokenRequestModel()
        {
            TokenType = TokenType.client;
        }
    }
}