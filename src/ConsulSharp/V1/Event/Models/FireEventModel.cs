using System.Collections.Generic;

namespace ConsulSharp.V1.Event.Models
{
    /// <summary>
    /// Event fire options.
    /// </summary>
    public class FireEventModel : EventFilterModel
    {
        /// <summary>
        /// Specifies the datacenter to query. 
        /// This will default to the datacenter of the agent being queried.
        /// </summary>
        public string DataCenter { get; set; }

        /// <summary>
        /// Specifies a raw payload.
        /// </summary>
        public string RawPayload { get; set; }

        internal new string ToQueryString()
        {
            var list = new List<string>();

            if (!string.IsNullOrWhiteSpace(DataCenter))
            {
                list.Add("dc=" + DataCenter);
            }

            if (!string.IsNullOrWhiteSpace(Node))
            {
                list.Add("node=" + Node);
            }

            if (!string.IsNullOrWhiteSpace(Service))
            {
                list.Add("service=" + Service);
            }

            if (!string.IsNullOrWhiteSpace(Tag))
            {
                list.Add("tag=" + Tag);
            }

            if (list.Count > 0)
            {
                return "?" + string.Join("&", list);
            }

            return string.Empty;
        }
    }
}