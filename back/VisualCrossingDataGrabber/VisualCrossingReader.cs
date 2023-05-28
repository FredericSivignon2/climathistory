﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("WeatherAPITests")]
namespace VisualCrossingDataGrabber
{
    // Weather Query Builder
    // https://www.visualcrossing.com/weather/weather-data-services/valence?v=api

    internal class VisualCrossingReader
    {
        private const string urlTemplate = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{0}/{1}-01-01/{1}-12-31?unitGroup=metric&elements=datetime%2Ctempmax%2Ctempmin%2Ctemp&include=days%2Cobs&key=3BXU58SLQQXU4CTK9YEEVCVFN&options=nonulls&contentType=json";
        private static readonly HttpClient client = new();

        public async Task<string> ReadAsync(int year, string townName)
        {
            Console.WriteLine("Reading data for year: " + year);
            var url = string.Format(urlTemplate, townName, year);
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("    -> Data successfully read.");
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} {response.ReasonPhrase}");
                return string.Empty;
            }
        }
    }
}
