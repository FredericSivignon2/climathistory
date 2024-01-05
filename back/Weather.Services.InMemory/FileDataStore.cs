using System.Diagnostics.Metrics;
using System.Text;
using System.Text.Json;

namespace Weather.Services.VisualCrossing
{
    internal class FileDataStore: IFileDataStore
    {
        #region Data Members
        private const string sourceDirectory = "D:\\Development\\Web\\React\\climathistorydata";
        private static JsonSerializerOptions _jsonOptions = new()
        {
            AllowTrailingCommas = true
        };
        #endregion

        #region Constructors
        public FileDataStore() 
        {
        }
        #endregion

        #region IFileDataStore interface implementation
        public IEnumerable<string> GetAllCountriesNames()
        {
            return Directory.EnumerateDirectories(sourceDirectory, "*", SearchOption.TopDirectoryOnly).Select(countryDirectory =>
                countryDirectory.Substring(countryDirectory.LastIndexOf('\\') + 1));
        }

        public IEnumerable<string> GetAllLocationsNames(string countryName)
        {
            var locationDirectories = GetLocationsDirectories(countryName);
            return locationDirectories.Select(locationDirectory => locationDirectory.Substring(locationDirectory.LastIndexOf('\\') + 1));
        }

        public VisualCrossingLocationFileData? GetDataPerLocation(string countryName, string locationName)
        {
            var locationDirectory = GetLocationDirectory(countryName, locationName);
            if (locationDirectory == null) return null;
            int fileCount = 0;
            VisualCrossingLocationFileData vcData = GetLocationData(ref fileCount, locationDirectory);
            Console.WriteLine($"  -> {fileCount} json files read for location {locationName}.");
            return vcData;
        }

        public VisualCrossingLocationFileData[] GetDataPerCountry(string country)
        {
            Console.Write($"Reading location information for country {country}");
            int fileCount = 0;
            var result = new List<VisualCrossingLocationFileData>();
            var locationDirectories = GetLocationsDirectories(country);
            var locationCount = locationDirectories.Count();
            foreach (var locationDirectory in locationDirectories)
            {
                VisualCrossingLocationFileData vcData = GetLocationData(ref fileCount, locationDirectory);
                result.Add(vcData);
            }
            Console.WriteLine($"  -> {locationCount} locations found and {fileCount} json files read.");
            return result.ToArray();
        }

        private static VisualCrossingLocationFileData GetLocationData(ref int fileCount, string locationDirectory)
        {
            string location = locationDirectory.Substring(locationDirectory.LastIndexOf('\\') + 1);
            VisualCrossingLocationFileData vcData = new(location);
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

            return vcData;
        }
        #endregion

        #region Private Methods
        private string GetCountryDirectory(string country)
        {
            return Path.Combine(sourceDirectory, country);
        }

        private string? GetLocationDirectory(string countryName, string locationName)
        {
            string countryDirectory = GetCountryDirectory(countryName);
            if (!Directory.Exists(countryDirectory))
            {
                return null;
            }
            return Path.Combine(countryDirectory, locationName);
        }
       
        private IEnumerable<string> GetLocationsDirectories(string countryName)
        {
            string countryDirectory = GetCountryDirectory(countryName);
            if (!Directory.Exists(countryDirectory))
            {
                return [];
            }

            return Directory.EnumerateDirectories(countryDirectory, "*", SearchOption.TopDirectoryOnly);
        }

        private static int LoadTemperatures(string dataTypeDirectory, VisualCrossingLocationFileData vcData)
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
                    var source = JsonSerializer.Deserialize<VisualCrossingTemperatureFileData>(json, _jsonOptions);
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
        #endregion
    }
}
