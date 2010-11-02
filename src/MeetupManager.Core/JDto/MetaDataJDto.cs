using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MeetupManager.Core.JDto
{
    [JsonObject]
    public class MetaDataJDto
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("lat")]
        public string Lat { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("lon")]
        public string Lon { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("next")]
        public string Next { get; set; }//??
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        [JsonProperty("updated")]
        //[JsonConverter(typeof(IsoDateTimeConverter))]
        public string Updated { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}