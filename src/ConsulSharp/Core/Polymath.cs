using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsulSharp.Core
{
    internal class Polymath
    {
        private const string ConsulTokenHeaderKey = "X-Consul-Token";
        private const string ConsulWrapTimeToLiveHeaderKey = "X-Consul-Wrap-TTL";

        private readonly HttpClient _httpClient;

        public ConsulClientSettings ConsulClientSettings { get; }

        public Polymath(ConsulClientSettings consulClientSettings)
        {
            ConsulClientSettings = consulClientSettings;

            var handler = new HttpClientHandler();
            consulClientSettings.PostProcessHttpClientHandlerAction?.Invoke(handler);

            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = new Uri(consulClientSettings.ConsulServerUriWithPort);

            if (consulClientSettings.ConsulServiceTimeout != null)
            {
                _httpClient.Timeout = consulClientSettings.ConsulServiceTimeout.Value;
            }
        }

        public async Task MakeConsulApiRequest(string resourcePath, HttpMethod httpMethod, object requestData = null, bool rawResponse = false, bool unauthenticated = false)
        {
            await MakeConsulApiRequest<JToken>(resourcePath, httpMethod, requestData, rawResponse, unauthenticated: unauthenticated);
        }

        public async Task<TResponse> MakeConsulApiRequest<TResponse>(string resourcePath, HttpMethod httpMethod, object requestData = null, bool rawResponse = false, Action<HttpResponseMessage> postResponseAction = null, string wrapTimeToLive = null, bool unauthenticated = false) where TResponse : class
        {
            var headers = new Dictionary<string, string>();

            if (!unauthenticated)
            {
                headers.Add(ConsulTokenHeaderKey, ConsulClientSettings.ConsulToken);
            }

            return await MakeRequestAsync<TResponse>(resourcePath, httpMethod, requestData, headers, rawResponse, postResponseAction).ConfigureAwait(ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        /// //////

        protected async Task<TResponse> MakeRequestAsync<TResponse>(string resourcePath, HttpMethod httpMethod, object requestData = null, IDictionary<string, string> headers = null, bool rawResponse = false, Action<HttpResponseMessage> postResponseAction = null) where TResponse : class
        {
            try
            {
                var requestUri = new Uri(_httpClient.BaseAddress, resourcePath);

                var requestContent = requestData != null
                    ? new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8)
                    : null;

                HttpRequestMessage httpRequestMessage = null;

                switch (httpMethod.ToString().ToUpperInvariant())
                {
                    case "GET":

                        httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
                        break;

                    case "DELETE":

                        httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);
                        break;

                    case "POST":

                        httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
                        {
                            Content = requestContent
                        };

                        break;

                    case "PUT":

                        httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri)
                        {
                            Content = requestContent
                        };

                        break;

                    case "HEAD":

                        httpRequestMessage = new HttpRequestMessage(HttpMethod.Head, requestUri);
                        break;

                    default:
                        throw new NotSupportedException("The Http Method is not supported: " + httpMethod);
                }

                if (headers != null)
                {
                    foreach (var kv in headers)
                    {
                        httpRequestMessage.Headers.Remove(kv.Key);
                        httpRequestMessage.Headers.Add(kv.Key, kv.Value);
                    }
                }

                ConsulClientSettings.BeforeApiRequestAction?.Invoke(_httpClient, httpRequestMessage);

                var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage).ConfigureAwait(ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

                // internal delegate.
                postResponseAction?.Invoke(httpResponseMessage);

                ConsulClientSettings.AfterApiResponseAction?.Invoke(httpResponseMessage);

                var responseText =
                    await
                        httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (!string.IsNullOrWhiteSpace(responseText))
                    {
                        var response = rawResponse ? (responseText as TResponse) : JsonConvert.DeserializeObject<TResponse>(responseText);
                        return response;
                    }

                    return default(TResponse);
                }

                throw new ConsulApiException(httpResponseMessage.StatusCode, responseText);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (ex.Response is HttpWebResponse response)
                    {
                        string responseText;

                        using (var stream = new StreamReader(response.GetResponseStream()))
                        {
                            responseText =
                                await stream.ReadToEndAsync().ConfigureAwait(ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
                        }

                        throw new ConsulApiException(response.StatusCode, responseText);
                    }

                    throw;
                }

                throw;
            }
        }
    }
}
