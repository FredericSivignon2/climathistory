using System.Diagnostics.Metrics;
using Weather.Application.Model;
using Weather.Application.Query;
using Weather.Database;
using Weather.Database.Model;

namespace DatabaseFeeder.Services
{
    internal class FeederService : IFeederService
    {
        private ITemperatureInfo _temperatureInfo;
        private ILocationInfo _locationInfo;
        private IWeatherRepository _weatherRepository;

        public FeederService(ITemperatureInfo temperatureInfo, ILocationInfo locationInfo, IWeatherRepository weatherRepository)
        {
            _temperatureInfo = temperatureInfo;
            _locationInfo = locationInfo;
            _weatherRepository = weatherRepository;
        }

        public async Task Feed()
        {
            try
            {
                var countries = await AddCountries();
                var locations = await AddLocations(countries);
                await AddTemperaturesInfo(countries, locations);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                _weatherRepository.Dispose();
            }
        }

        private async Task<IEnumerable<CountryData>> AddCountries()
        {
            var countries = await _locationInfo.GetAllCountries();
            foreach (var country in countries)
            {
                var isExistingCountry = await _weatherRepository.IsExistingCountry(country.Name);
                if (!isExistingCountry)
                {
                    Console.WriteLine($"Adding new country {country.Name}...");
                    await _weatherRepository.AddCountryAsync(new CountryData() { Name = country.Name });
                }
            }
            return await _weatherRepository.GetAllCountriesAsync();
        }

        private async Task<IEnumerable<LocationData>> AddLocations(IEnumerable<CountryData> countries)
        {
            foreach (var country in countries)
            {
                var locations = _locationInfo.GetAllLocationsByCountry(country.Name);
                foreach (var location in locations)
                {
                    var isExistingLocation = await _weatherRepository.IsExistingLocation(country.CountryId, location.Name);
                    if (!isExistingLocation)
                    {
                        Console.WriteLine($"Adding new location {location.Name} for country {country.Name}...");
                        await _weatherRepository.AddLocationAsync(new LocationData() { Name = location.Name, CountryId = country.CountryId });
                    }
                }
            }
            return await _weatherRepository.GetAllLocationsAsync();
        }

        private async Task AddTemperaturesInfo(IEnumerable<CountryData> countries, IEnumerable<LocationData> locations)
        {
            int addedCount = 0, updatedCount = 0, intermediateUpdateCount = 0;

            foreach (var location in locations)
            {
                var currentCountry = countries.First(country => country.CountryId == location.CountryId);
                var locationInfo = _temperatureInfo.GetLocationInfoFrom(currentCountry.Name, location.Name);

                var temperaturesToAdd = new List<TemperaturesData>(20000);
                intermediateUpdateCount = 0;
                foreach (YearInfoModel yim in locationInfo.YearsInfo)
                {
                    foreach (DayInfoModel dim in yim.Days)
                    {
                        var temperatureData = await _weatherRepository.GetTemperaturesAsync(location.LocationId, dim.Date);
                        if (temperatureData == null)
                        {
                            temperaturesToAdd.Add(new TemperaturesData()
                            {
                                LocationId = location.LocationId,
                                Date = dim.Date,
                                AvgTemperature = Convert.ToDecimal(dim.TempMean),
                                MinTemperature = Convert.ToDecimal(dim.TempMax),
                                MaxTemperature = Convert.ToDecimal(dim.TempMin)
                            });
                        }
                        else
                        {
                            // If there is something to update
                            if (temperatureData.AvgTemperature != dim.TempMean ||
                                temperatureData.MinTemperature != dim.TempMax ||
                                temperatureData.MaxTemperature != dim.TempMin)
                            {
                                temperatureData.AvgTemperature = dim.TempMean;
                                temperatureData.MinTemperature = dim.TempMax;
                                temperatureData.MaxTemperature = dim.TempMin;
                                await _weatherRepository.UpdateTemperatureAsync(temperatureData);
                                intermediateUpdateCount++;
                            }
                        }
                    }
                }
                if (temperaturesToAdd.Any())
                {
                    await _weatherRepository.AddTemperaturesBulkAsync(temperaturesToAdd);
                    addedCount += temperaturesToAdd.Count;
                    updatedCount += intermediateUpdateCount;
                    Console.WriteLine($"{temperaturesToAdd.Count} temperatures info added and {intermediateUpdateCount} updated for location {location.Name}.");
                }
            }
            Console.WriteLine($"TOTAL: {addedCount} temperatures info added and {updatedCount} updated.");
        }
    }
}
