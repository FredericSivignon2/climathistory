using System.Text.Encodings.Web;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Weather.Application.Query;
using Weather.Application.Model;
using Weather.Application.Models;
using Npgsql;
using Weather.Database;
using Weather.Database.Extensions;

namespace Weather.Services.InMemory
{
    public class WeatherServiceDatabase : ITemperatureInfo, ILocationInfo
    {
        private IWeatherRepository _repository;

        public WeatherServiceDatabase(IWeatherRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CountryModel>> GetAllCountries()
        {
            return (await _repository.GetAllCountriesAsync()).MapToCountryModels();
        }

        public async Task<IEnumerable<LocationModel>> GetAllLocationsByCountry(long countryId)
        {
            return (await _repository.GetAllLocationsAsync(countryId)).MapToLocationModels();
        }

        public YearInfoModel GetTemperaturesInfoFrom(string countryName, string townName, int year)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MeanPerYearModel> GetAverageTemperaturesDataFrom(string countryName, string locationName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MinMaxPerYearModel> GetMinMaxTemperaturesDataFrom(string countryName, string locationName)
        {
            throw new NotImplementedException();
        }

        public LocationInfoModel GetLocationInfoFrom(string countryName, string locationName)
        {
            throw new NotImplementedException();
        }
    }
}
