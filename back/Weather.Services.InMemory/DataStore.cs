using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Weather.Services.InMemory
{
    internal class DataStore: IDataStore
    {
        private const bool allCountriesAvailable = false;
        private string[] availableCountries = { "France" };
        private const string sourceDirectory = "D:\\Development\\Web\\React\\climathistorydata";
        private Dictionary<string, Dictionary<string, VisualCrossingData>> _data = new();
        private static JsonSerializerOptions _jsonOptions = new()
        {
            AllowTrailingCommas = true
        };
        
        public DataStore() {
            Load();
        }

        public Dictionary<string, Dictionary<string, VisualCrossingData>> Data { get { return _data; } }

        public void Load()
        {
            Console.WriteLine("Loading data from disk...");
            int locationCount = 0, fileCount = 0;
            var countryDirectories = Directory.EnumerateDirectories(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
            foreach (var countryDirectory in countryDirectories)
            {
                string country = countryDirectory.Substring(countryDirectory.LastIndexOf('\\') + 1);
                if (!allCountriesAvailable && !availableCountries.Contains(country)) continue;
                Console.Write($"    -> Loading data for {country}");
                var countryData = new Dictionary<string, VisualCrossingData>();
                _data.Add(country, countryData);

                var locationDirectories = Directory.EnumerateDirectories(countryDirectory, "*", SearchOption.TopDirectoryOnly);
                locationCount = locationDirectories.Count();
                foreach (var locationDirectory in locationDirectories)
                {
                    string location = locationDirectory.Substring(locationDirectory.LastIndexOf('\\') + 1);
                    VisualCrossingData vcData = new(location);
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


        private static int LoadTemperatures(string dataTypeDirectory, VisualCrossingData vcData)
        {
            int readCount = 0;
            byte progressIndex = 0;
            char[] progressChars = new char[] { '|', '/', '-', '\\' };
            Console.Write(" ");

            var jsonFiles = Directory.EnumerateFiles(dataTypeDirectory, "*.json", SearchOption.TopDirectoryOnly);
            foreach (var jsonFile in jsonFiles)
            {
                using (StreamReader r = new StreamReader(jsonFile, Encoding.UTF8))
                {
                    string json = r.ReadToEnd();
                    var source = JsonSerializer.Deserialize<VisualCrossingTemperatureData>(json, _jsonOptions);
                    if (source != null)
                    {
                        vcData.Temperatures.Add(source);
                        readCount++;
                        Console.Write($"\b{progressChars[progressIndex++]}");
                        if (progressIndex >= progressChars.Length)
                        {
                            progressIndex = 0;
                        }
                    }
                }

            }
            Console.Write("\b");
            return readCount;
        }
    }
}
