namespace ConsulSharp.V1.ACL.LegacyToken
{
    /// <summary>
    /// The Token Request model.
    /// </summary>
    public class TokenRequest : AbstractToken
    {
        /// <summary>
        /// Initializes an instance of <see cref="TokenRequest"/>.
        /// </summary>
        public TokenRequest()
        {
            TokenType = TokenType.client;
        }
    }
}