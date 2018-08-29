using ConsulSharp.V1.ACL;
using ConsulSharp.V1.Event;
using ConsulSharp.V1.KeyValue;
using ConsulSharp.V1.Snapshot;
using ConsulSharp.V1.Status;
using ConsulSharp.V1.Transaction;

namespace ConsulSharp.V1
{
    /// <summary>
    /// The V1 interface for the Consul Api.
    /// </summary>
    public interface IConsulClientV1
    {
        /// <summary>
        /// The ACL interface.
        /// </summary>
        IACL ACL { get; }

        /// <summary>
        /// The Event interface.
        /// </summary>
        IEvent Event { get; }

        /// <summary>
        /// The KeyValue interface.
        /// </summary>
        IKeyValue KeyValue { get; }

        /// <summary>
        /// The Snapshot interface.
        /// </summary>
        ISnapshot Snapshot { get; }

        /// <summary>
        /// The Status interface.
        /// </summary>
        IStatus Status { get; }

        /// <summary>
        /// The Transaction interface.
        /// </summary>
        ITransaction Transaction { get; }
    }
}
