using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsulSharp.V1.ACL.Models
{
    public class AuthRequest
    {
        /// <summary>
        /// The name of the auth method to use for login.
        /// </summary>
        [JsonProperty("AuthMethod")]
        public string AuthMethodName { get; set; }

        /// <summary>
        /// The bearer token to present to the auth method during login for authentication purposes. 
        /// For the Kubernetes auth method this is a Service Account Token (JWT).
        /// </summary>
        [JsonProperty("BearerToken")]
        public string BearerToken { get; set; }

        /// <summary>
        /// Specifies arbitrary KV metadata linked to the token. Can be useful to track origins.
        /// </summary>
        [JsonProperty("Meta")]
        public Dictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// (Enterprise Only) Specifies the namespace of the Auth Method to use for Login. 
        /// If not provided in the JSON body, the value of the ns URL query parameter 
        /// or in the X-Consul-Namespace header will be used. 
        /// If not provided at all, the namespace will be inherited from the request's 
        /// ACL token, or will default to the default namespace. Added in Consul 1.7.0.
        /// </summary>
        [JsonProperty("Namespace")]
        public string Namespace { get; set; }
    }
}