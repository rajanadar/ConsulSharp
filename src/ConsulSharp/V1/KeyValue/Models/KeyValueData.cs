using System.Collections.Generic;

namespace ConsulSharp.V1.KeyValue.Models
{
    /// <summary>
    /// Key value data.
    /// </summary>
    public class KeyValueData
    {
        /// <summary>
        /// The Key values.
        /// </summary>
        public List<KeyValueModel> KeyValueModels { get; set; }

        /// <summary>
        /// The Keys.
        /// </summary>
        public List<string> Keys { get; set; }

        /// <summary>
        /// The raw value.
        /// </summary>
        public string RawValue { get; set; }
    }
}