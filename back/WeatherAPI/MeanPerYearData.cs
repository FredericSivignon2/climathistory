using System.Text.Json.Serialization;

namespace WeatherAPI
{

    public class MeanPerYearData
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }
        /// <summary>
        /// Mean of the all year (mean of mean per day)
        /// </summary>
        [JsonPropertyName("mean")]
        public double Mean { get; set; }
        /// <summary>
        /// Mean of the max temperatures per day
        /// </summary>
        [JsonPropertyName("meanMax")]
        public double MeanMax { get; set; }
        /// <summary>
        /// Mean of min temperatures per day
        /// </summary>
        [JsonPropertyName("meanMin")]
        public double MeanMin { get; set; }
    }
}
