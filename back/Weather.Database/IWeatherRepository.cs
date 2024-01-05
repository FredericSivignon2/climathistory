using System.Data;
using Weather.Database.Model;

namespace Weather.Database
{
    public interface IWeatherRepository : IDisposable
    {
        Task AddCountryAsync(CountryData country);
        Task AddTemperatureAsync(TemperaturesData temperature);
        Task AddTemperaturesBulkAsync(IEnumerable<TemperaturesData> temperatures);
        /// <summary>
        /// Adds a new location into the database.
        /// </summary>
        /// <param name="location">The location object to add.</param>
        /// <returns>The auto-generated identifier of the created location record.</returns>
        Task<long> AddLocationAsync(LocationData location);

        Task<LocationData?> GetLocation(long countryId, string name);

        Task<CountryData> GetCountryByNameAsync(string name);
        Task<bool> IsExistingCountry(string name);
        Task<IEnumerable<CountryData>> GetAllCountriesAsync();
        Task<bool> IsExistingLocation(long countryId, string name);
        Task<IEnumerable<LocationData>> GetAllLocationsAsync();
        Task<IEnumerable<LocationData>> GetAllLocationsAsync(long countryId);

        Task<IEnumerable<TemperaturesData>> GetTemperaturesAsync(long locationId);
        Task<TemperaturesData?> GetTemperaturesAsync(long locationId, DateTime date);
        Task<IEnumerable<TemperaturesData>> GetTemperaturesAsync(long locationId, int year);

        Task UpdateCountryAsync(CountryData country);
        Task UpdateTemperatureAsync(TemperaturesData temperature);
        Task UpdateTemperaturesBulkAsync(IEnumerable<TemperaturesData> temperatures);
        Task UpdateLocationAsync(LocationData town);

        Task<IEnumerable<AverageTemperaturesByYearData>> GetAvgTemperaturesForAllYearsDataAsync(long locationId);
        Task<IEnumerable<MinMaxTemperaturesByYearData>> GetMinMaxTemperaturesForAllYearsDataAsync(long locationId);
    }
}