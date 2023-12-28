using System.Data;
using Weather.Database.Model;

namespace Weather.Database
{
    public interface IWeatherRepository : IDisposable
    {
        Task AddCountryAsync(CountryData country);
        Task AddTemperatureAsync(TemperaturesData temperature);
        Task AddTemperaturesBulkAsync(IEnumerable<TemperaturesData> temperatures);
        Task AddLocationAsync(LocationData town);
        Task<CountryData> GetCountryByNameAsync(string name);
        Task<bool> IsExistingCountry(string name);
        Task<IEnumerable<CountryData>> GetAllCountriesAsync();
        Task<bool> IsExistingLocation(long countryId, string name);
        Task<IEnumerable<LocationData>> GetAllLocationsAsync();
        Task<IEnumerable<LocationData>> GetAllLocationsAsync(long countryId);
        Task<IEnumerable<TemperaturesData>> GetTemperaturesAsync(long locationId);
        Task<TemperaturesData?> GetTemperaturesAsync(long locationId, DateTime date);
        Task UpdateCountryAsync(CountryData country);
        Task UpdateTemperatureAsync(TemperaturesData temperature);
        Task UpdateLocationAsync(LocationData town);
    }
}