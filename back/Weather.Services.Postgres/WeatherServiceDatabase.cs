using Weather.Application.Query;
using Weather.Application.Model;
using Weather.Application.Models;
using Weather.Database;
using Weather.Database.Extensions;

namespace Weather.Services.VisualCrossing
{
    public class WeatherServiceDatabase : IWeatherReader
    {
        private IWeatherRepository _repository;

        public WeatherServiceDatabase(IWeatherRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Health()
        {
            try
            {
                await _repository.GetCountriesCountAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<CountryModel>> GetAllCountries()
        {
            return (await _repository.GetAllCountriesAsync()).MapToCountryModels();
        }

        public async Task<IEnumerable<LocationModel>> GetAllLocationsByCountry(long countryId)
        {
            return (await _repository.GetAllLocationsAsync(countryId)).MapToLocationModels();
        }

        public async Task<TemperaturesYearInfoModel> GetTemperaturesInfoFrom(long locationId, int year)
        {
            return (await _repository.GetTemperaturesAsync(locationId, year)).MapToYearInfoModel(year);
        }

        public async Task<TemperaturesLocationInfoModel> GetTemperaturesLocationInfoFrom(long locationId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AveragePerYearModel>> GetAvgTemperaturesForAllYears(long locationId)
        {
            return (await _repository.GetAvgTemperaturesForAllYearsDataAsync(locationId)).MapToAveragePerYearModels();
        }

        public async Task<IEnumerable<MinMaxPerYearModel>> GetMinMaxTemperaturesDataFrom(long locationId)
        {
            return (await _repository.GetMinMaxTemperaturesForAllYearsDataAsync(locationId)).MapToMinMaxPerYearModels();
        }

        public async Task<TemperatureModel?> GetAverageTemperatureByDateRange(long locationId, DateTime startDate, DateTime endDate)
        {
            return (await _repository.GetAverageTemperatureByDateRangeAsync(locationId, startDate, endDate)).MapToTemperatureModel();
        }

        public async Task<TemperaturesLocationInfoModel> GetLocationInfoFrom(long locationId)
        {
            throw new NotImplementedException();
        }
    }
}
