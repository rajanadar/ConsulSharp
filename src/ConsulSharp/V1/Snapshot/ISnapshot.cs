using System.Threading.Tasks;
using ConsulSharp.V1.Commons;
using ConsulSharp.V1.Snapshot.Models;

namespace ConsulSharp.V1.Snapshot
{
    /// <summary>
    /// The Snapshot interface.
    /// </summary>
    public interface ISnapshot
    {
        /// <summary>
        /// This endpoint generates and returns an atomic, point-in-time snapshot of the Consul server state.
        /// Snapshots are exposed as gzipped tar archives which internally contain the Raft metadata required to restore, 
        /// as well as a binary serialized version of the Consul server state.
        /// The contents are covered internally by SHA-256 hashes.
        /// These hashes are verified during snapshot restore operations.
        /// The structure of the archive is internal to Consul and not intended to be used other than for restore operations.
        /// The archives are not designed to be modified before a restore.
        /// </summary>
        /// <param name="request">The request with generate options.</param>
        /// <returns>The gzip snapshot bytes.</returns>
        Task<ConsulResponse<byte[]>> GenerateAsync(ConsulRequest<SnapshotRequestModel> request = null);

        /// <summary>
        /// This endpoint restores a point-in-time snapshot of the Consul server state.
        /// Restores involve a potentially dangerous low-level Raft operation that is not designed to handle server failures during a restore.
        /// This operation is primarily intended to be used when recovering from a disaster, restoring into a fresh cluster of Consul servers.
        /// </summary>
        /// <param name="request">The restore request.</param>
        /// <returns>The response.</returns>
        Task<ConsulResponse> RestoreAsync(ConsulRequest<SnapshotRestoreModel> request);
    }
}