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

        public VisualCrossingLocationFileData[] GetDataPerCountry(string country)
        {
            Console.Write($"Reading location information for country {country}");
            int fileCount = 0;
            string countryDirectory = GetCountryDirectory(country);
            var result = new List<VisualCrossingLocationFileData>();
            if (!Directory.Exists(countryDirectory))
            {
                return [];
            }

            var locationDirectories = Directory.EnumerateDirectories(countryDirectory, "*", SearchOption.TopDirectoryOnly);
            var locationCount = locationDirectories.Count();
            foreach (var locationDirectory in locationDirectories)
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
                result.Add(vcData);
            }
            Console.WriteLine($"  -> {locationCount} locations found and {fileCount} json files read.");
            return result.ToArray();
        }
        #endregion

        #region Private Methods
        private string GetCountryDirectory(string country)
        {
            return Path.Combine(sourceDirectory, country);
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
