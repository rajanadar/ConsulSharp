using Newtonsoft.Json;

namespace ConsulSharp.V1.Event.Models
{
    /// <summary>
    /// The Event model.
    /// </summary>
    public class EventModel
    {
        /// <summary>
        /// The Id.
        /// </summary>
        [JsonProperty("ID")]
        public string Id { get; set; }

        /// <summary>
        /// The name.
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// The payload.
        /// </summary>
        [JsonProperty("Payload")]
        public string Payload { get; set; }

        /// <summary>
        /// The node filter.
        /// </summary>
        [JsonProperty("NodeFilter")]
        public string NodeFilter { get; set; }

        /// <summary>
        /// The service filter.
        /// </summary>
        [JsonProperty("ServiceFilter")]
        public string ServiceFilter { get; set; }

        /// <summary>
        /// The tag filter.
        /// </summary>
        [JsonProperty("TagFilter")]
        public string TagFilter { get; set; }

        /// <summary>
        /// The version.
        /// </summary>
        [JsonProperty("Version")]
        public long Version { get; set; }

        /// <summary>
        /// The time.
        /// </summary>
        [JsonProperty("LTime")]
        public long LTime { get; set; }
    }
}