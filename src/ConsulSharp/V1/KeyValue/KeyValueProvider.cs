using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;
using ConsulSharp.V1.KeyValue.Models;

namespace ConsulSharp.V1.KeyValue
{
    internal class KeyValueProvider : IKeyValue
    {
        private readonly Polymath _polymath;

        public KeyValueProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<bool>> WriteAsync(ConsulRequest<WriteKeyValueModel> request)
        {
            var response = await _polymath.MakeConsulApiRequest<string>(request, "v1/kv/" + request.RequestData.Key.TrimStart('/') + request.RequestData.ToQueryString(), HttpMethod.Put, request.RequestData.Value, rawRequest: true, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
            return response.Map(() => bool.Parse(response.Data));
        }

        public async Task<ConsulResponse<KeyValueData>> ReadAsync(ConsulRequest<ReadKeyValueModel> request)
        {
            var resourcePath = "v1/kv/" + request.RequestData.Key.TrimStart('/') + request.RequestData.ToQueryString();

            if (request.RequestData.KeysOnly)
            {
                var keysResponse = await _polymath.MakeConsulApiRequest<List<string>>(request, resourcePath, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
                return keysResponse.Map(() => new KeyValueData { Keys = keysResponse.Data });
            }

            if (request.RequestData.RawResponse)
            {
                var rawResponse = await _polymath.MakeConsulApiRequest<string>(request, resourcePath, HttpMethod.Get, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
                return rawResponse.Map(() => new KeyValueData { RawValue = rawResponse.Data });
            }

            var response = await _polymath.MakeConsulApiRequest<List<KeyValueModel>>(request, resourcePath, HttpMethod.Get).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
            return response.Map(() => new KeyValueData { KeyValueModels = response.Data });
        }
    }
}