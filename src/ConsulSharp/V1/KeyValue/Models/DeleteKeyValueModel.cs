using System.Collections.Generic;

namespace ConsulSharp.V1.KeyValue.Models
{
    /// <summary>
    /// Model to delete keys.
    /// </summary>
    public class DeleteKeyValueModel
    {
        /// <summary>
        /// Specifies the path of the key to delete.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Specifies to delete all keys which have the specified prefix. 
        /// Without this, only a key with an exact match will be deleted.
        /// </summary>
        public bool Recurse { get; set; }

        /// <summary>
        /// Specifies to use a Check-And-Set operation. 
        /// This is very useful as a building block for more complex synchronization primitives. 
        /// If the index is 0, Consul will only put the key if it does not already exist. 
        /// If the index is non-zero, the key is only set if the index matches the ModifyIndex of that key.
        /// </summary>
        public int CheckAndSet { get; set; }

         internal string ToQueryString()
        {
            var list = new List<string>();

            if (Recurse)
            {
                list.Add("recurse");
            }

            if (CheckAndSet > 0)
            {
                list.Add("cas=" + CheckAndSet);
            }
            
            if (list.Count > 0)
            {
                return "?" + string.Join("&", list);
            }

            return string.Empty;
        }
    }
}