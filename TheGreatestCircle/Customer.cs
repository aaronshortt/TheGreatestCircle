using System;
using System.Collections.Generic;
using System.Net;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheGreatestCircle
{
    public partial class Customer
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        public override string ToString()
        {
            return "Customer Name: " + Name + "\t" + "Customer ID: " + "\t" + UserId;
        }
    }


    public partial class Customer
    {
        public static Customer FromJson(string json) => JsonConvert.DeserializeObject<Customer>(json, TheGreatestCircle.Converter.Settings);
    }


    public static class Serialize
    {
        public static string ToJson(this Customer self) => JsonConvert.SerializeObject(self, TheGreatestCircle.Converter.Settings);
    }

    internal class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter()
                {
                    DateTimeStyles = DateTimeStyles.AssumeUniversal,
                },
            },
        };
    }
}