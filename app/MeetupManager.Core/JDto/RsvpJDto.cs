using System;
using System.Collections.Generic;
using System.Globalization;
using MeetupManager.Core.Domain;
using Newtonsoft.Json;
using PublicDomain;

namespace MeetupManager.Core.JDto
{
    [JsonObject]
    public class RsvpJDto
    {
        [JsonProperty("meta")]
        public MetaDataJDto Meta { get; set; }
        [JsonProperty("results")]
        public IList<RsvpItemJDto> Results { get; set; }

        [JsonObject]
        public class RsvpItemJDto
        {
            [JsonProperty("answers")]
            public IList<string> Answers { get; set; }
            [JsonProperty("city")]
            public string City { get; set; }
            [JsonProperty("comment")]
            public string Comment { get; set; }
            [JsonProperty("coord")]
            public decimal Coord { get; set; }
            [JsonProperty("country")]
            public string Country { get; set; }
            [JsonProperty("created")]
            [JsonConverter(typeof(JsonLocalDateConverter))]
            public DateTime Created { get; set; }
            [JsonProperty("guests")]
            public int Guests { get; set; }
            [JsonProperty("link")]
            public string Link { get; set; }
            [JsonProperty("lon")]
            public decimal Lon { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("response")]
            public RsvpReponseType Response { get; set; }
            [JsonProperty("state")]
            public string State { get; set; }
            [JsonProperty("updated")]
            [JsonConverter(typeof(JsonLocalDateConverter))]
            public DateTime Updated { get; set; }
            [JsonProperty("zip")]
            public string Zip { get; set; }
        }
    }

    public class JsonLocalDateConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType)
        {
            //"Fri Jun 05 05:32:37 EDT 2009" orginal
            //"Jun 05 05:32:37 2009" //removed
            //"MMM dd hh:mm:ss yyyy"

            string date = reader.Value.ToString().Substring(4).Remove(16, 4);

            var exact = DateTime.ParseExact(date, "MMM dd HH:mm:ss yyyy", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal);
            return exact;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
    }
}