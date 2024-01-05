using System.Runtime.CompilerServices;
using log4net;
using Microsoft.Extensions.Options;

[assembly: InternalsVisibleTo("WeatherAPITests")]
namespace VisualCrossingDataGrabber.Services
{
    // Weather Query Builder
    // https://www.visualcrossing.com/weather/weather-data-services/valence?v=api

    internal class VisualCrossingReader : IVisualCrossingReader
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(VisualCrossingReader));
        private const string urlTemplate = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{0}/{1}-01-01/{1}-12-31?unitGroup=metric&elements=datetime%2Ctempmax%2Ctempmin%2Ctemp&include=days%2Cobs&key={2}&options=nonulls&contentType=json";
        private static readonly HttpClient client = new();
        private GrabberSettings _settings;

        public VisualCrossingReader(IOptions<GrabberSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<string> ReadLocationInfoPerYearAsync(int year, string townName)
        {
            log.Info("Reading data for year: " + year);
            var url = string.Format(urlTemplate, townName, year, _settings.VisualCrossingKey);
            Uri uri = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                log.Info("    -> Data successfully read.");
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} {response.ReasonPhrase}");
            }
        }
    }
}
