using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ConsulSharp.V1
{
    /// <summary>
    /// The Token Request model.
    /// </summary>
    public class TokenRequestModel
    {
        /// <summary>
        /// <para>[optional] for new Token requests. If not provided, a new UUID will be generated.</para>
        /// <para>[required] for update Token requests.</para>
        /// The ID of the ACL.
        /// </summary>
        [JsonProperty("ID")]
        public string Id { get; set; }

        /// <summary>
        /// <para>[optional]</para>
        /// Specifies a human-friendly name for the ACL token.
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// <para>[required] Defaults to <see cref="TokenType.client"/>.</para>
        /// Specifies the type of ACL token. Valid values are from <see cref="TokenType"/>.
        /// </summary>
        [JsonProperty("Type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TokenType TokenType { get; set; }

        /// <summary>
        /// <para>[optional]</para>
        /// Specifies rules for this ACL token. The format of the Rules property is documented in the ACL Guide.
        /// https://www.consul.io/docs/guides/acl.html
        /// </summary>
        [JsonProperty("Rules")]
        public string Rules { get; set; }

        /// <summary>
        /// Initializes an instance of <see cref="TokenRequestModel"/>.
        /// </summary>
        public TokenRequestModel()
        {
            TokenType = TokenType.client;
        }
    }
}