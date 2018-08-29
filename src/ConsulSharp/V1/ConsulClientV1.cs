using ConsulSharp.Core;
using ConsulSharp.V1.ACL;
using ConsulSharp.V1.Event;
using ConsulSharp.V1.KeyValue;
using ConsulSharp.V1.Session;
using ConsulSharp.V1.Snapshot;
using ConsulSharp.V1.Status;
using ConsulSharp.V1.Transaction;

namespace ConsulSharp.V1
{
    internal class ConsulClientV1 : IConsulClientV1
    {
        public ConsulClientV1(Polymath polymath)
        {
            ACL = new ACLProvider(polymath);
            Event = new EventProvider(polymath);
            KeyValue = new KeyValueProvider(polymath);
            Session = new SessionProvider(polymath);
            Snapshot = new SnapshotProvider(polymath);
            Status = new StatusProvider(polymath);
            Transaction = new TransactionProvider(polymath);
        }

        public IACL ACL { get; }

        public IEvent Event { get; }

        public IKeyValue KeyValue { get; }

        public ISession Session { get; }

        public ISnapshot Snapshot { get; }

        public IStatus Status { get; }

        public ITransaction Transaction { get; }
    }
}
