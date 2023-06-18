using System.Text.Json.Serialization;

namespace WeatherAPI
{
    public class MinMaxPerYearData
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("min")]
        public double Min { get; set; }

        [JsonPropertyName("max")]
        public double Max { get; set; }
    }
}
