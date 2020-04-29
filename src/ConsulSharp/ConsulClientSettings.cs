using System;
using System.Net.Http;

namespace ConsulSharp
{
    /// <summary>
    /// The consul client settings.
    /// </summary>
    public class ConsulClientSettings
    {
        /// <summary>
        /// Constructor with bare minimal required fields.
        /// </summary>
        /// <param name="consulServerUriWithPort"></param>
        /// <param name="consulToken"></param>
        public ConsulClientSettings(string consulServerUriWithPort, string consulToken)
        {
            ConsulServerUriWithPort = consulServerUriWithPort;
            ConsulToken = consulToken;
        }

        /// <summary>
        /// The Consul Server Uri with port.
        /// </summary>
        public string ConsulServerUriWithPort { get; }

        /// <summary>
        /// The auth method to be used to acquire a consul token.
        /// </summary>
        public string ConsulToken { get; }

        /// <summary>
        /// Flag to indicate async context.
        /// </summary>
        public bool ContinueAsyncTasksOnCapturedContext { get; set; }

        /// <summary>
        /// The Api timeout.
        /// </summary>
        public TimeSpan? ConsulServiceTimeout { get; set; }

        /// <summary>
        /// The one time http client's http client handler delegate.
        /// Use this to set proxy settings etc.
        /// </summary>
        public Action<HttpClientHandler> PostProcessHttpClientHandlerAction { get; set; }

        /// <summary>
        /// The per http request delegate invoked before every consul api http request.
        /// </summary>
        public Action<HttpClient, HttpRequestMessage> BeforeApiRequestAction { get; set; }

        /// <summary>
        /// The per http response delegate invoked after every consul api http response.
        /// </summary>
        public Action<HttpResponseMessage> AfterApiResponseAction { get; set; }

        /// <summary>
        /// Flag to indicate how the consul token should be passed to the API.
        /// Default is to use the Authorization: Bearer &lt;consul-token&gt; scheme.
        /// </summary>
        public bool UseConsulTokenHeaderInsteadOfAuthorizationHeader { get; set; }

        /// <summary>
        /// The namespace to use to achieve tenant level isolation.
        /// </summary>
        public string GlobalNamespace { get; set; }

        /// <summary>
        /// A factory delegate to use if you want to provide your own http client.
        /// The Handler already has the certificates etc. enabled. 
        /// Don't worry about setting any consul specific values on your http client.
        /// Just create your http client and pass it in. 
        /// ConsulSharp will set all the necessary things.
        /// Use the handler parameter to set proxy etc. 
        /// It is essential that your HttpClient use the handler, if you have custom proxy settings etc.
        /// </summary>
        public Func<HttpClientHandler, HttpClient> MyHttpClientProviderFunc { get; set; }
    }
}
