using System.Collections.Generic;

namespace ConsulSharp.V1.KeyValue.Models
{
    /// <summary>
    /// Model to read key value.
    /// </summary>
    public class ReadKeyValueModel
    {
        /// <summary>
        /// Specifies the path of the key to read.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Specifies the datacenter to query. 
        /// This will default to the datacenter of the agent being queried. 
        /// </summary>
        public string DataCenter { get; set; }

        /// <summary>
        /// Specifies if the lookup should be recursive and key treated as a prefix instead of a literal match.
        /// </summary>
        public bool Recurse { get; set; }

        /// <summary>
        /// Specifies the response is just the raw value of the key, without any encoding or metadata.
        /// </summary>
        public bool RawResponse { get; set; }

        /// <summary>
        /// Specifies to return only keys (no values or metadata). Specifying this implies recurse.
        /// </summary>
        public bool KeysOnly { get; set; }

        /// <summary>
        /// Specifies the character to use as a separator for recursive lookups. 
        /// </summary>
        public string Separator { get; set; }

        internal string ToQueryString()
        {
            var list = new List<string>();

            if (!string.IsNullOrWhiteSpace(DataCenter))
            {
                list.Add("dc=" + DataCenter);
            }

            if (Recurse)
            {
                list.Add("recurse");
            }

            if (RawResponse)
            {
                list.Add("raw");
            }

            if (KeysOnly)
            {
                list.Add("keys");
            }

            if (!string.IsNullOrWhiteSpace(Separator))
            {
                list.Add("separator=" + Separator);
            }

            if (list.Count > 0)
            {
                return "?" + string.Join("&", list);
            }

            return string.Empty;
        }
    }
}