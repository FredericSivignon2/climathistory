using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Weather.Services.VisualCrossing
{
    public class VisualCrossingLocationFileData
    {
        public VisualCrossingLocationFileData(string location)
        {
            Location = location;
            Temperatures = new List<VisualCrossingTemperatureFileData>();
        }

        public string Location { get; private set; }
        public IList<VisualCrossingTemperatureFileData> Temperatures { get; private set; }
    }

    public class VisualCrossingTemperatureFileData
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
        public List<VisualCrossingDaysFileData>? Days { get; set; }
    }

    public class VisualCrossingDaysFileData
    {
        [JsonPropertyName("datetime")]
        public DateTime Datetime { get; set; }
        [JsonPropertyName("tempmax")]
        public decimal Tempmax { get; set; }
        [JsonPropertyName("tempmin")]
        public decimal Tempmin { get; set; }
        [JsonPropertyName("temp")]
        public decimal Temp { get; set; }
    }
}
