using System.Collections.Generic;

namespace ConsulSharp.V1.Event.Models
{
    /// <summary>
    /// Event filter model.
    /// </summary>
    public class EventFilterModel
    {
        /// <summary>
        /// Specifies the name of the event to fire. 
        /// This name must not start with an underscore, since those are reserved for Consul internally.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Specifies a regular expression to filter by node name.
        /// </summary>
        public string Node { get; set; }

        /// <summary>
        /// Specifies a regular expression to filter by service name.
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// Specifies a regular expression to filter by tag.
        /// </summary>
        public string Tag { get; set; }

        internal string ToQueryString()
        {
            var list = new List<string>();

            if (!string.IsNullOrWhiteSpace(Name))
            {
                list.Add("name=" + Name);
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