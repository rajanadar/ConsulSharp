using ConsulSharp.Core;

namespace ConsulSharp.V1
{
    internal class ConsulClientV1 : IConsulClientV1
    {
        public ConsulClientV1(Polymath polymath)
        {
            ACL = new ACLProvider(polymath);
        }

        public IACL ACL { get; }
    }
}
