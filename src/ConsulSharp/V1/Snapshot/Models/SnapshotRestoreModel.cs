using System.Collections.Generic;

namespace ConsulSharp.V1.Snapshot.Models
{
    /// <summary>
    /// Restore snapshot.
    /// </summary>
    public class SnapshotRestoreModel
    {
        /// <summary>
        /// Specifies the datacenter to query. This will default to the datacenter of the agent being queried.
        /// </summary>
        public string Datacenter { get; set; }

        /// <summary>
        /// A snapshot archive byte array.
        /// </summary>
        public byte[] Snapshot { get; set; }

        internal string ToQueryString()
        {
            var list = new List<string>();

            if (!string.IsNullOrWhiteSpace(Datacenter))
            {
                list.Add("dc=" + Datacenter);
            }

            if (list.Count > 0)
            {
                return "?" + string.Join("&", list);
            }

            return string.Empty;
        }
    }
}