using System.Text.Json;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace WeatherAPI
{
    public class WeartherService
    {
        private const string sourceDirectory = "D:\\Development\\Web\\React\\climathistorydata";
        private Dictionary<string, Dictionary<string, VisualCrossingData>> _data = new();

        public WeartherService()
        {
        }

        public IDictionary<string, Dictionary<string, VisualCrossingData>> Data
        {
            get { return _data; }
        }

        public string GetAllCountries()
        {
            return JsonSerializer.Serialize<List<string>>(_data.Keys.ToList<string>(), GetJsonSerializerOptions());
        }

        public string GetAllLocationsByCountry(string countryName)
        {
            List<string> towns = new();

            if (!_data.ContainsKey(countryName))
            {
                return string.Empty;
            }
            var countryData = _data[countryName];
            foreach (VisualCrossingData data in countryData.Values)
            {
                towns.Add(data.Town);
            }
            return JsonSerializer.Serialize<List<string>>(towns, GetJsonSerializerOptions());
        }

        public string GetTemperaturesDataFrom(string countryName, string townName, int year)
        {
            Console.WriteLine($"Reading temperatures for {townName} in {year}");

            var vcd = GetVisualCrossingDataFrom(countryName, townName);
            if (vcd == null)
            {
                return string.Empty;
            }
            var temperatures = vcd.Temperatures.First<VisualCrossingTemperatureData>(tdata =>
            {
                return tdata.Year == year;
            });
            return JsonSerializer.Serialize<VisualCrossingTemperatureData>(temperatures, GetJsonSerializerOptions());
        }

        public string GetAverageTemperaturesDataFrom(string countryName, string locationName)
        {
            Console.WriteLine($"Reading average temperatures for {locationName}");
            var vcd = GetVisualCrossingDataFrom(countryName, locationName);
            if (vcd == null)
            {
                return string.Empty;
            }
            List<MeanPerYearData> meanData = new();
            foreach (VisualCrossingTemperatureData tData in vcd.Temperatures)
            {
                meanData.Add(new MeanPerYearData()
                {
                    Year = tData.Year,
                    Mean = tData.Days.Average(d => d.Temp),
                    MeanMax = tData.Days.Average(d => d.Tempmax),
                    MeanMin = tData.Days.Average(d => d.Tempmin)
                }
                );

            }
            return JsonSerializer.Serialize<List<MeanPerYearData>>(meanData, GetJsonSerializerOptions());
        }

        public string GetMinMaxTemperaturesDataFrom(string countryName, string locationName)
        {
            Console.WriteLine($"Reading min max temperatures for {locationName}");
            var vcd = GetVisualCrossingDataFrom(countryName, locationName);
            if (vcd == null)
            {
                return string.Empty;
            }
            List<MinMaxPerYearData> minMaxData = new();
            foreach (VisualCrossingTemperatureData tData in vcd.Temperatures)
            {
                minMaxData.Add(new MinMaxPerYearData()
                {
                    Year = tData.Year,
                    Min = tData.Days.Min(d => d.Tempmin),
                    Max = tData.Days.Max(d => d.Tempmax)
                }
                );

            }
            return JsonSerializer.Serialize<List<MinMaxPerYearData>>(minMaxData, GetJsonSerializerOptions());
        }

        public void Load()
        {
            Console.WriteLine("Loading data from disk...");
            int locationCount = 0, fileCount = 0;
            var countryDirectories = Directory.EnumerateDirectories(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
            foreach (var countryDirectory in countryDirectories)
            {
                string country = countryDirectory.Substring(countryDirectory.LastIndexOf('\\') + 1);
                Console.Write($"    -> Loading data for {country}");
                var countryData = new Dictionary<string, VisualCrossingData>();
                _data.Add(country, countryData);

                var locationDirectories = Directory.EnumerateDirectories(countryDirectory, "*", SearchOption.TopDirectoryOnly);
                locationCount = locationDirectories.Count();
                foreach (var locationDirectory in locationDirectories)
                {
                    string location = locationDirectory.Substring(locationDirectory.LastIndexOf('\\') + 1);
                    VisualCrossingData vcData = new VisualCrossingData(location);
                    Console.Write(".");
                    var dataTypeDirectories = Directory.EnumerateDirectories(locationDirectory, "*", SearchOption.TopDirectoryOnly);
                    foreach (var dataTypeDirectory in dataTypeDirectories)
                    {
                        string dataType = dataTypeDirectory.Substring(dataTypeDirectory.LastIndexOf('\\') + 1);
                        switch (dataType)
                        {
                            case "Temperatures":
                                fileCount += LoadTemperatures(dataTypeDirectory, vcData);
                                break;
                            default:
                                break;
                        }
                    }
                    countryData.Add(location, vcData);
                }

                Console.WriteLine();
            }
            Console.WriteLine($"  -> {locationCount} locations found and {fileCount} json files read.");
        }

        private VisualCrossingData? GetVisualCrossingDataFrom(string countryName, string locationName)
        {
            if (!_data.ContainsKey(countryName))
            {
                return null;
            }
            var countryData = _data[countryName];
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

        private int LoadTemperatures(string dataTypeDirectory, VisualCrossingData vcData)
        {
            int readCount = 0;
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.AllowTrailingCommas = true;
            var jsonFiles = Directory.EnumerateFiles(dataTypeDirectory, "*.json", SearchOption.TopDirectoryOnly);
            foreach (var jsonFile in jsonFiles)
            {
                using (StreamReader r = new StreamReader(jsonFile, Encoding.UTF8))
                {
                    string json = r.ReadToEnd();
                    var source = JsonSerializer.Deserialize<VisualCrossingTemperatureData>(json, options);
                    if (source != null)
                    {
                        vcData.Temperatures.Add(source);
                        readCount++;
                    }
                }

            }
            return readCount;
        }
    }
}
