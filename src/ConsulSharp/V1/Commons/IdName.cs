using Newtonsoft.Json;

namespace ConsulSharp.V1.Commons
{
    /// <summary>
    /// Represents a IdName
    /// </summary>
    public class IdName
    {
        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}