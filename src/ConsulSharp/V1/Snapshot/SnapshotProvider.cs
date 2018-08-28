using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;
using ConsulSharp.V1.Snapshot.Models;
using Newtonsoft.Json.Linq;

namespace ConsulSharp.V1.Snapshot
{
    internal class SnapshotProvider : ISnapshot
    {
        private readonly Polymath _polymath;

        public SnapshotProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<byte[]>> GenerateAsync(ConsulRequest<SnapshotRequestModel> request = null)
        {
            return await _polymath.MakeConsulApiRequest<byte[]>(request, "v1/snapshot" + ((request != null && request.RequestData != null) ? request.RequestData.ToQueryString() : string.Empty), HttpMethod.Get, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }

        public async Task<ConsulResponse> RestoreAsync(ConsulRequest<SnapshotRestoreModel> request)
        {
            return await _polymath.MakeConsulApiRequest<JToken>(request, "v1/snapshot" + request.RequestData.ToQueryString(), HttpMethod.Put, request.RequestData.Snapshot, rawRequest: true, rawResponse: true).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);
        }
    }
}