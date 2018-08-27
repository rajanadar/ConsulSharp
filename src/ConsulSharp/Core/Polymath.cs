using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsulSharp.Core
{
    internal class Polymath
    {
        private const string ConsulTokenHeaderKey = "X-Consul-Token";
        private const string ConsulIndexHeaderKey = "X-Consul-Index";
        private const string ConsulLastContactHeaderKey = "X-Consul-LastContact";
        private const string ConsulKnownLeaderHeaderKey = "X-Consul-KnownLeader";

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

        public async Task MakeConsulApiRequest(ConsulRequest request, string resourcePath, HttpMethod httpMethod, object requestData = null, bool rawRequest = false, bool rawResponse = false, bool unauthenticated = false)
        {
            await MakeConsulApiRequest<JToken>(request, resourcePath, httpMethod, requestData, rawRequest, rawResponse, unauthenticated: unauthenticated);
        }

        public async Task<ConsulResponse<TResponseData>> MakeConsulApiRequest<TResponseData>(ConsulRequest request, string resourcePath, HttpMethod httpMethod, object requestData = null, bool rawRequest = false, bool rawResponse = false, Action<HttpResponseMessage> postResponseAction = null, bool unauthenticated = false) where TResponseData : class
        {
            var headers = new Dictionary<string, string>();

            if (!unauthenticated)
            {
                headers.Add(ConsulTokenHeaderKey, ConsulClientSettings.ConsulToken);
            }

            return await MakeRequestAsync<TResponseData>(request, resourcePath, httpMethod, requestData, headers, rawRequest, rawResponse, postResponseAction).ConfigureAwait(ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        private async Task<ConsulResponse<TResponseData>> MakeRequestAsync<TResponseData>(ConsulRequest request, string resourcePath, HttpMethod httpMethod, object requestData = null, IDictionary<string, string> headers = null, bool rawRequest = false, bool rawResponse = false, Action<HttpResponseMessage> postResponseAction = null) where TResponseData : class
        {
            try
            {
                if (request != null)
                {
                    if (request.Index != null)
                    {
                        var kv = "index=" + request.Index.Value;
                        var joiner = resourcePath.Contains("?") ? "&" : "?";

                        resourcePath = resourcePath + joiner + kv;
                    }

                    if (!string.IsNullOrWhiteSpace(request.Wait))
                    {
                        var kv = "wait=" + request.Wait;
                        var joiner = resourcePath.Contains("?") ? "&" : "?";

                        resourcePath = resourcePath + joiner + kv;
                    }

                    if (request.ConsistencyMode != ConsistencyMode.@default)
                    {
                        var joiner = resourcePath.Contains("?") ? "&" : "?";
                        resourcePath = resourcePath + joiner + request.ConsistencyMode.ToString();
                    }

                    if (request.PrettyJsonResponse)
                    {
                        var joiner = resourcePath.Contains("?") ? "&" : "?";
                        resourcePath = resourcePath + joiner + "pretty";
                    }
                }

                var requestUri = new Uri(_httpClient.BaseAddress, resourcePath);

                var requestContent = requestData != null
                    ? new StringContent((rawRequest ? requestData.ToString() : JsonConvert.SerializeObject(requestData)), Encoding.UTF8)
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

                var response = new ConsulResponse<TResponseData>();

                IEnumerable<string> values;

                if (httpResponseMessage.Headers.TryGetValues(ConsulIndexHeaderKey, out values) && values != null)
                {
                    // for cross platform, use the iterator instead of linq stuff.
                    foreach(var value in values)
                    {
                        response.Index = int.Parse(value, CultureInfo.InvariantCulture);
                        break;
                    }
                }

                values = null;

                if (httpResponseMessage.Headers.TryGetValues(ConsulLastContactHeaderKey, out values) && values != null)
                {
                    foreach (var value in values)
                    {
                        response.LastContactMilliseconds = long.Parse(value);
                        break;
                    }
                }

                values = null;

                if (httpResponseMessage.Headers.TryGetValues(ConsulKnownLeaderHeaderKey, out values) && values != null)
                {
                    foreach (var value in values)
                    {
                        response.KnownLeader = bool.Parse(value);
                        break;
                    }
                }

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (!string.IsNullOrWhiteSpace(responseText))
                    {
                        response.Data = rawResponse ? (responseText as TResponseData) : JsonConvert.DeserializeObject<TResponseData>(responseText);
                    }

                    return response;
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
