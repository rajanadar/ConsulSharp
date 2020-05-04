using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json.Linq;

namespace ConsulSharp.V1.ACL.Agent.Connect
{
    internal class ConnectProvider : IConnect
    {
        private readonly Polymath _polymath;

        public ConnectProvider(Polymath polymath)
        {
            _polymath = polymath;
        }

        public Task<ConsulResponse<ConnectAuthResponse>> AuthorizeAsync(ConsulRequest<ConnectAuthRequest> request)
        {
            throw new NotImplementedException();
        }

        public Task<ConsulResponse<CARootsResponse>> GetCARootsAsync(ConsulRequest request = null)
        {
            throw new NotImplementedException();
        }

        public Task<ConsulResponse<CertificateModel>> GetLeafCertificateAsync(ConsulRequest<string> request)
        {
            throw new NotImplementedException();
        }
    }
}