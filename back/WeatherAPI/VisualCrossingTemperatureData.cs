﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherAPI
{
    public class VisualCrossingData
    {
        public VisualCrossingData(string location)
        {
            Town = location;
            Temperatures = new List<VisualCrossingTemperatureData>();
        }

        public string Town { get; private set; }
        public IList<VisualCrossingTemperatureData> Temperatures { get; private set; }
    }

    public class VisualCrossingTemperatureData
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }
        [JsonPropertyName("queryCost")]
        public int QueryCost { get; set; }
        [JsonPropertyName("latitude")]
        public float Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public float Longitude { get; set; }
        [JsonPropertyName("resolvedAddress")]
        public string? ResolvedAddress { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }
        [JsonPropertyName("tzoffset")]
        public float Tzoffset { get; set; }
        [JsonPropertyName("days")]
        public List<VisualCrossingDays>? Days { get; set; }
    }

    public class VisualCrossingDays
    {
        [JsonPropertyName("datetime")]
        public string? Datetime { get; set; }
        [JsonPropertyName("tempmax")]
        public float Tempmax { get; set; }
        [JsonPropertyName("tempmin")]
        public float Tempmin { get; set; }
        [JsonPropertyName("temp")]
        public float Temp { get; set; }
    }
}
