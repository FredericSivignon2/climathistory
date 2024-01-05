using Microsoft.Extensions.Options;
using Weather.Application.VisualCrossing.Model;
using Weather.Application.VisualCrossing.Queries;
using Weather.Database;
using Weather.Database.Model;

namespace DatabaseFeeder.Services
{
    internal class FeederService : IFeederService
    {
        private readonly ImportSettings _settings;
        private IVisualCrossingReader _locationInfo;
        private IWeatherRepository _weatherRepository;

        public FeederService(IVisualCrossingReader locationInfo, IWeatherRepository weatherRepository, IOptions<ImportSettings> settings)
        {
            _locationInfo = locationInfo;
            _weatherRepository = weatherRepository;
            _settings = settings.Value;
        }

        public async Task Feed()
        {
            try
            {
                var countries = await AddCountries();
                await AddLocations(countries);
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
            bool noCountryAdded = true;
            foreach (var country in countries)
            {
                var isExistingCountry = await _weatherRepository.IsExistingCountry(country.Name);
                if (!isExistingCountry)
                {
                    Console.WriteLine($"Adding new country {country.Name}...");
                    await _weatherRepository.AddCountryAsync(new CountryData() { Name = country.Name });
                    noCountryAdded = false;
                }
            }
            if (noCountryAdded)
            {
                Console.WriteLine("No new country added.");
            }
            return await _weatherRepository.GetAllCountriesAsync();
        }

        private async Task AddLocations(IEnumerable<CountryData> countries)
        {
            bool noLocationAdded = true;
            foreach (var country in countries)
            {
                var locationsNames = _locationInfo.GetAllLocationsNames(country.Name);
                foreach (var locationName in locationsNames)
                {
                    var locationData = await _weatherRepository.GetLocation(country.CountryId, locationName);
                    if (locationData != null && _settings.SkipExistingLocations) continue;

                    var location = _locationInfo.GetLocationByCountry(country.Name, locationName);

                    long locationId;
                    if (locationData == null)
                    {
                        Console.WriteLine($"Adding new location {locationName} for country {country.Name}...");
                        locationId = await _weatherRepository.AddLocationAsync(new LocationData() { Name = locationName, CountryId = country.CountryId });
                        noLocationAdded = false;
                    }
                    else
                    {
                        locationId = locationData.LocationId;
                    }

                    if (location != null)
                    {
                        await AddOrUpdateTemperaturesForLocation(location, locationId);
                    }
                }
            }
            if (noLocationAdded)
            {
                Console.WriteLine("No new location added.");
            }
        }

        private async Task AddOrUpdateTemperaturesForLocation(VisualCrossingLocationModel location, long locationId)
        {
            var temperaturesToAdd = new List<TemperaturesData>(20000);
            var temperaturesToUpdate = new List<TemperaturesData>(20000);
            foreach (VisualCrossingYearTemperatureModel yim in location.TemperaturesPerYear)
            {
                foreach (VisualCrossingDayTemperatureModel dim in yim.TemperaturesPerDay)
                {
                    var temperatureData = await _weatherRepository.GetTemperaturesAsync(locationId, dim.Date);
                    if (temperatureData == null)
                    {
                        temperaturesToAdd.Add(new TemperaturesData()
                        {
                            LocationId = locationId,
                            Date = dim.Date,
                            AvgTemperature = Convert.ToDecimal(dim.TemperatureAverage),
                            MinTemperature = Convert.ToDecimal(dim.TemperatureMin),
                            MaxTemperature = Convert.ToDecimal(dim.TemperatureMax)
                        });
                    }
                    else
                    {
                        // If there is something to update
                        if (temperatureData.AvgTemperature != dim.TemperatureAverage ||
                            temperatureData.MinTemperature != dim.TemperatureMin ||
                            temperatureData.MaxTemperature != dim.TemperatureMax)
                        {
                            temperatureData.AvgTemperature = dim.TemperatureAverage;
                            temperatureData.MinTemperature = dim.TemperatureMin;
                            temperatureData.MaxTemperature = dim.TemperatureMax;
                            await _weatherRepository.UpdateTemperatureAsync(temperatureData);
                            temperaturesToUpdate.Add(temperatureData);
                        }
                    }
                }
            }
            if (temperaturesToAdd.Any())
            {
                await _weatherRepository.AddTemperaturesBulkAsync(temperaturesToAdd);
                Console.WriteLine($"{temperaturesToAdd.Count} temperatures info added for location {location.Name}.");
            }
            if (temperaturesToUpdate.Any())
            {
                await _weatherRepository.UpdateTemperaturesBulkAsync(temperaturesToUpdate);
                Console.WriteLine($"{temperaturesToUpdate.Count} temperatures info updated for location {location.Name}.");
            }
        }
    }
}
