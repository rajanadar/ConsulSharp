using System.Collections.Generic;

namespace ConsulSharp.V1.Session.Models
{
    /// <summary>
    /// The node session read model.
    /// </summary>
    public class NodeSessionReadModel
    {
        /// <summary>
        /// Specifies the datacenter to query. 
        /// This will default to the datacenter of the agent being queried.
        /// Using this across datacenters is not recommended.
        /// </summary>
        public string DataCenter { get; set; }

        /// <summary>
        /// Specifies the name or ID of the node to query.
        /// </summary>
        public string Node { get; set; }

        internal string ToQueryString()
        {
            var list = new List<string>();

            if (!string.IsNullOrWhiteSpace(DataCenter))
            {
                list.Add("dc=" + DataCenter);
            }

            if (list.Count > 0)
            {
                return "?" + string.Join("&", list);
            }

            return string.Empty;
        }
    }
}