using Microsoft.Extensions.Options;
using System.Diagnostics;
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
                //if (country.Name.ToLower() != "france")
                //    continue;

                var locationsNames = _locationInfo.GetAllLocationsNames(country.Name);
                foreach (var locationName in locationsNames)
                {
                    var locationData = await _weatherRepository.GetLocation(country.CountryId, locationName);
                    if (locationData != null && _settings.SkipExistingLocations) continue;

                    var location = _locationInfo.GetLocationByCountry(country.Name, locationName);
                    if (location == null) continue; // Should not happen

                    long locationId;
                    if (locationData == null)
                    {
                        Console.WriteLine($"Adding new location {locationName} for country {country.Name}...");
                        locationId = await _weatherRepository.AddLocationAsync(new LocationData() { Name = locationName, CountryId = country.CountryId });
                        noLocationAdded = false;
                        // If the location has not already exist, it means we can add all temperatures in on shot, without trying to
                        // see if there is missing one. Because AddOrUpdateTemperaturesForLocation is very slow
                        await AddAllTemperaturesForLocation(location, locationId);

                        // WARNING: Due to this optimization, we will need another option to perform an update of temperatures
                        // values... perhaps an update of a specific location only, to avoid to re-scan everything
                    }
                    else
                    {
                        locationId = locationData.LocationId;
                        await AddOrUpdateTemperaturesForLocation(location, locationId);
                    }
                }
            }
            if (noLocationAdded)
            {
                Console.WriteLine("No new location added.");
            }
        }

        private async Task AddAllTemperaturesForLocation(VisualCrossingLocationModel location, long locationId)
        {
            var temperaturesToAdd = new List<TemperaturesData>(20000);
            foreach (VisualCrossingYearTemperatureModel yim in location.TemperaturesPerYear)
            {
                foreach (VisualCrossingDayTemperatureModel dim in yim.TemperaturesPerDay)
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
            }
            if (temperaturesToAdd.Any())
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                await _weatherRepository.AddTemperaturesBulkAsync(temperaturesToAdd);
                stopWatch.Stop();
                Console.WriteLine($"{temperaturesToAdd.Count} temperatures info added for location {location.Name} in {stopWatch.Elapsed.TotalSeconds} seconds.");
            }
        }

        // Only needed if the feeder app has been stopped before a location has been fully added
        private async Task AddOrUpdateTemperaturesForLocation(VisualCrossingLocationModel location, long locationId)
        {
            var temperaturesData = await _weatherRepository.GetTemperaturesAsync(locationId);
            if (temperaturesData == null)
                return;
            var temperaturesDataDic = new Dictionary<DateTime, TemperaturesData>(20000);
            foreach (var temperatureData in temperaturesData)
            {
                temperaturesDataDic.Add(temperatureData.Date, temperatureData);
            }

            Console.WriteLine($"There are already {temperaturesData.Count()} temperatures ");

            var temperaturesToAdd = new List<TemperaturesData>(20000);
            var temperaturesToUpdate = new List<TemperaturesData>(20000);
            foreach (VisualCrossingYearTemperatureModel yim in location.TemperaturesPerYear)
            {
                foreach (VisualCrossingDayTemperatureModel dim in yim.TemperaturesPerDay)
                {
                    var temperatureData = temperaturesDataDic.ContainsKey(dim.Date) ? temperaturesDataDic[dim.Date] : null;
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
                Stopwatch stopWatch = Stopwatch.StartNew();
                await _weatherRepository.AddTemperaturesBulkAsync(temperaturesToAdd);
                stopWatch.Stop();
                Console.WriteLine($"{temperaturesToAdd.Count} temperatures info added for location {location.Name} in {stopWatch.Elapsed.TotalSeconds} seconds.");
            }
            if (temperaturesToUpdate.Any())
            {
                await _weatherRepository.UpdateTemperaturesBulkAsync(temperaturesToUpdate);
                Console.WriteLine($"{temperaturesToUpdate.Count} temperatures info updated for location {location.Name}.");
            }
        }

    }
}
