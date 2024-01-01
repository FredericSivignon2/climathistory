using Weather.Application.VisualCrossing.Model;
using Weather.Application.VisualCrossing.Queries;
using Weather.Database;
using Weather.Database.Model;

namespace DatabaseFeeder.Services
{
    internal class FeederService : IFeederService
    {
        private IVisualCrossingReader _locationInfo;
        private IWeatherRepository _weatherRepository;

        public FeederService(IVisualCrossingReader locationInfo, IWeatherRepository weatherRepository)
        {
            _locationInfo = locationInfo;
            _weatherRepository = weatherRepository;
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

        private async Task AddLocations(IEnumerable<CountryData> countries)
        {
            foreach (var country in countries)
            {
                var locations = _locationInfo.GetAllLocationsByCountry(country.Name);
                foreach (var location in locations)
                {
                    long locationId = 0;
                    var locationData = await _weatherRepository.GetLocation(country.CountryId, location.Name);
                    if (locationData == null)
                    {
                        Console.WriteLine($"Adding new location {location.Name} for country {country.Name}...");
                        locationId = await _weatherRepository.AddLocationAsync(new LocationData() { Name = location.Name, CountryId = country.CountryId });
                    }
                    else
                    {
                        locationId = locationData.LocationId;
                    }

                    await AddTemperaturesForLocation(location, locationId);
                }
            }
        }

        private async Task AddTemperaturesForLocation(VisualCrossingLocationModel location, long locationId)
        {
            int intermediateUpdateCount = 0;
            var temperaturesToAdd = new List<TemperaturesData>(20000);
            intermediateUpdateCount = 0;
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
                            temperatureData.MinTemperature != dim.TemperatureMax ||
                            temperatureData.MaxTemperature != dim.TemperatureMin)
                        {
                            temperatureData.AvgTemperature = dim.TemperatureAverage;
                            temperatureData.MinTemperature = dim.TemperatureMin;
                            temperatureData.MaxTemperature = dim.TemperatureMax;
                            await _weatherRepository.UpdateTemperatureAsync(temperatureData);
                            intermediateUpdateCount++;
                        }
                    }
                }
            }
            if (temperaturesToAdd.Any())
            {
                await _weatherRepository.AddTemperaturesBulkAsync(temperaturesToAdd);
                Console.WriteLine($"{temperaturesToAdd.Count} temperatures info added and {intermediateUpdateCount} updated for location {location.Name}.");
            }
        }
    }
}
