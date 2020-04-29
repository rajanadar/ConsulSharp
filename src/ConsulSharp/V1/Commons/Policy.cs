using Newtonsoft.Json;

namespace ConsulSharp.V1.Commons
{
    /// <summary>
    /// Represents a Consul Policy.
    /// </summary>
    public class Policy
    {
        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}