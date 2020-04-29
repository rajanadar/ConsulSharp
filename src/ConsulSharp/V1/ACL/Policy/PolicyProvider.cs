using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;

namespace ConsulSharp.V1.ACL.Policy
{
    internal class PolicyProvider : IPolicy
    {
        private readonly Polymath _polymath;

        public PolicyProvider(Polymath polymath)
        {
            _polymath = polymath;
        }
    }
}