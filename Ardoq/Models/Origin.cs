using System;
using Newtonsoft.Json;

namespace Ardoq.Models
{
    public class Origin
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public String Id { get; set; }

        [JsonProperty(PropertyName = "_version", NullValueHandling = NullValueHandling.Ignore)]
        public int? VersionCounter { get; set; }

        public override string ToString()
        {
            return "Origin{" +
                   "id='" + Id + '\'' +
                   ", _version=" + VersionCounter +
                   '}';
        }
    }
}