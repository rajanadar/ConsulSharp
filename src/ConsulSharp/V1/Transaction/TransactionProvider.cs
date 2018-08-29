using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;
using ConsulSharp.V1.Transaction.Models;

namespace ConsulSharp.V1.Transaction
{
    internal class TransactionProvider : ITransaction
    {
        private readonly Polymath _polymath;

        public TransactionProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public async Task<ConsulResponse<TransactionResponseModel>> CreateAsync(ConsulRequest<TransactionRequestModel> request)
        {
            var status = TransactionStatus.Success;

            var response = await _polymath.MakeConsulApiRequest<TransactionResponseModel>(request, "v1/txn" + request.RequestData.ToQueryString(), HttpMethod.Put, request.RequestData.Operations, postResponseFunc: httpResponseMessage =>
            {
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    status = TransactionStatus.Rollback;
                    return true;
                }

                return false;
            }
            ).ConfigureAwait(_polymath.ConsulClientSettings.ContinueAsyncTasksOnCapturedContext);

            response.Data.Status = status;
            return response;
        }
    }
}