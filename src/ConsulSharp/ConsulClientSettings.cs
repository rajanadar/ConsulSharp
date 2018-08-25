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
    }
}
