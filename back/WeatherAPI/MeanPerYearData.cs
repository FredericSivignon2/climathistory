using System.Text.Json.Serialization;

namespace WeatherAPI
{

    public class MeanPerYearData
    {
        [JsonPropertyName("year")]
        public int Year { get; init; }
        /// <summary>
        /// Mean of the all year (mean of mean per day)
        /// </summary>
        [JsonPropertyName("mean")]
        public double Mean { get; init; }
        /// <summary>
        /// Mean of the max temperatures per day
        /// </summary>
        [JsonPropertyName("meanMax")]
        public double MeanMax { get; init; }
        /// <summary>
        /// Mean of min temperatures per day
        /// </summary>
        [JsonPropertyName("meanMin")]
        public double MeanMin { get; init; }
    }
}
