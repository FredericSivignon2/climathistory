using System.Text.Json.Serialization;

namespace WeatherAPI
{
    public class MinMaxPerYearData
    {
        [JsonPropertyName("year")]
        public required int Year { get; init; }

        [JsonPropertyName("min")]
        public required double Min { get; init; }

        [JsonPropertyName("max")]
        public required double Max { get; init; }
    }
}
