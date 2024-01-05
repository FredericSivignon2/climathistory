using System.Text.Encodings.Web;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Weather.Application.VisualCrossing.Model;
using Weather.Application.VisualCrossing.Queries;

namespace Weather.Services.VisualCrossing
{
    public class WeatherServiceVisualCrossing : IVisualCrossingReader
    {
        private IFileDataStore _store;

        public WeatherServiceVisualCrossing(IFileDataStore store)
        {
            _store = store;
        }


        public async Task<IEnumerable<VisualCrossingCountryModel>> GetAllCountries()
        {
            return await Task.FromResult(_store.GetAllCountriesNames().Select(country => new VisualCrossingCountryModel(country)));
        }

        public IEnumerable<string> GetAllLocationsNames(string countryName)
        {
            return _store.GetAllLocationsNames(countryName);
        }

        public VisualCrossingLocationModel? GetLocationByCountry(string countryName, string locationName)
        {
            return _store.GetDataPerLocation(countryName, locationName)?.MapToVisualCrossingLocationModel(locationName);
        }

        public IEnumerable<VisualCrossingLocationModel> GetAllLocationsByCountry(string countryName)
        {
            var dataPerCountry = _store.GetDataPerCountry(countryName);
            return dataPerCountry.Select(locationData => locationData.MapToVisualCrossingLocationModel(locationData.Location));
        }
    }
}
