using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsulSharp.V1.Session.Models
{
    /// <summary>
    /// The session delete model.
    /// </summary>
    public class SessionDeleteModel
    {
        /// <summary>
        /// Specifies the datacenter to query. 
        /// This will default to the datacenter of the agent being queried.
        /// Using this across datacenters is not recommended.
        /// </summary>
        public string DataCenter { get; set; }

        /// <summary>
        /// Specifies the UUID of the session to destroy.
        /// </summary>
        public string SessionId { get; set; }

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