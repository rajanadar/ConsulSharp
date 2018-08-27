using ConsulSharp.Core;
using ConsulSharp.V1.ACL;
using ConsulSharp.V1.KeyValue;

namespace ConsulSharp.V1
{
    internal class ConsulClientV1 : IConsulClientV1
    {
        public ConsulClientV1(Polymath polymath)
        {
            ACL = new ACLProvider(polymath);

            KeyValue = new KeyValueProvider(polymath);
        }

        public IACL ACL { get; }

        public IKeyValue KeyValue { get; }
    }
}
