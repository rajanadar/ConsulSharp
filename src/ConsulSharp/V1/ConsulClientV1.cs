using ConsulSharp.Core;
using ConsulSharp.V1.ACL;
using ConsulSharp.V1.Event;
using ConsulSharp.V1.KeyValue;
using ConsulSharp.V1.Snapshot;
using ConsulSharp.V1.Status;

namespace ConsulSharp.V1
{
    internal class ConsulClientV1 : IConsulClientV1
    {
        public ConsulClientV1(Polymath polymath)
        {
            ACL = new ACLProvider(polymath);
            Event = new EventProvider(polymath);
            KeyValue = new KeyValueProvider(polymath);
            Snapshot = new SnapshotProvider(polymath);
            Status = new StatusProvider(polymath);
        }

        public IACL ACL { get; }

        public IEvent Event { get; }

        public IKeyValue KeyValue { get; }

        public ISnapshot Snapshot { get; }

        public IStatus Status { get; }
    }
}
