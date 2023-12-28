using System.Text.Encodings.Web;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Weather.Application.Query;
using Weather.Application.Model;
using Weather.Application.Models;

namespace Weather.Services.InMemory
{
    public class WeatherServiceInMemory : ITemperatureInfo, ILocationInfo
    {
        private IDataStore _store;

        public WeatherServiceInMemory(IDataStore store)
        {
            _store = store;
        }


        public async Task<IEnumerable<CountryModel>> GetAllCountries()
        {
            return await Task.FromResult(_store.Data.Keys.Select(country => new CountryModel(0, country)));
        }

        public IEnumerable<LocationModel> GetAllLocationsByCountry(string countryName)
        {
            List<LocationModel> towns = new();

            if (!_store.Data.ContainsKey(countryName))
            {
                return Enumerable.Empty<LocationModel>();
            }
            var countryData = _store.Data[countryName];
            foreach (VisualCrossingData data in countryData.Values)
            {
                towns.Add(new LocationModel(0, data.Town, 0));
            }
            return towns;
        }

        public LocationInfoModel GetLocationInfoFrom(string countryName, string locationName)
        {
            var vcd = GetVisualCrossingDataFrom(countryName, locationName);
            if (vcd == null)
            {
                return LocationInfoModel.Empty();
            }
            return vcd?.MapToLocationInfoModel() ?? LocationInfoModel.Empty();
        }

        public YearInfoModel GetTemperaturesInfoFrom(string countryName, string locationName, int year)
        {
            Console.WriteLine($"Reading temperatures for {locationName} in {year}");

            var vcd = GetVisualCrossingDataFrom(countryName, locationName);
            if (vcd == null)
            {
                return YearInfoModel.Empty();
            }
            var temperatures = vcd.Temperatures.FirstOrDefault<VisualCrossingTemperatureData>(tdata =>
            {
                return tdata.Year == year;
            });
            return temperatures?.MapToYearInfoModel() ?? YearInfoModel.Empty();
        }

        public IEnumerable<MeanPerYearModel> GetAverageTemperaturesDataFrom(string countryName, string locationName)
        {
            Console.WriteLine($"Reading average temperatures for {locationName}");
            var vcd = GetVisualCrossingDataFrom(countryName, locationName);
            if (vcd == null)
            {
                return Enumerable.Empty<MeanPerYearModel>();
            }

            return vcd.Temperatures.Select(v => new MeanPerYearModel(v.Year,
                v.Days.Average(d => d.Temp),
                v.Days.Average(d => d.Tempmax),
                v.Days.Average(d => d.Tempmin))).ToList();
        }

        public IEnumerable<MinMaxPerYearModel> GetMinMaxTemperaturesDataFrom(string countryName, string locationName)
        {
            Console.WriteLine($"Reading min max temperatures for {locationName}");
            var vcd = GetVisualCrossingDataFrom(countryName, locationName);
            if (vcd == null)
            {
                return Enumerable.Empty<MinMaxPerYearModel>();
            }

            return vcd.Temperatures.Select(t => new MinMaxPerYearModel(
                t.Year,
                t.Days.Min(d => d.Tempmin),
                t.Days.Max(d => d.Tempmax))).ToList();
        }

       
        private VisualCrossingData? GetVisualCrossingDataFrom(string countryName, string locationName)
        {
            if (!_store.Data.ContainsKey(countryName))
            {
                return null;
            }
            var countryData = _store.Data[countryName];
            if (!countryData.ContainsKey(locationName))
            {
                return null;
            }
            return countryData[locationName];
        }

        private JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
        }

    }
}
