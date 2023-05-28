using System.Text.Json;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.Extensions.Options;

namespace WeatherAPI
{
    public class WeartherService
    {
        private const string sourceDirectory = "D:\\Development\\Web\\React\\climathistorydata";
        private Dictionary<string, VisualCrossingData> _data = new();

        public WeartherService()
        {
        }

        public IDictionary<string, VisualCrossingData> Data
        {
            get { return _data; }
        }

        public string GetAllLocations()
        {
            List<string> locations = new();
            foreach (VisualCrossingData data in _data.Values)
            {
                locations.Add(data.Location);
            }
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            return JsonSerializer.Serialize<List<string>>(locations, options);
        }

        public string GetTemperaturesDataFrom(string locationName, int year)
        {
            Console.WriteLine($"Reading temperatures for {locationName} in {year}");
            locationName = locationName.ToLower();

            if (!_data.ContainsKey(locationName))
            {
                return String.Empty; // Considere to return '204 No content'
            }
            VisualCrossingData vcd = _data[locationName];

            var temperatures = vcd.Temperatures.First<VisualCrossingTemperatureData>(tdata =>
            {
                return tdata.Year == year;
            });
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            }; return JsonSerializer.Serialize<VisualCrossingTemperatureData>(temperatures, options);
        }

        public void Load()
        {

            var locationDirectories = Directory.EnumerateDirectories(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
            foreach (var locationDirectory in locationDirectories)
            {
                string location = locationDirectory.Substring(locationDirectory.LastIndexOf('\\') + 1);
                VisualCrossingData vcData = new VisualCrossingData(location);

                var dataTypeDirectories = Directory.EnumerateDirectories(locationDirectory, "*", SearchOption.TopDirectoryOnly);
                foreach(var dataTypeDirectory in dataTypeDirectories)
                {
                    string dataType = dataTypeDirectory.Substring(dataTypeDirectory.LastIndexOf('\\') + 1);
                    switch (dataType)
                    {
                        case "Temperatures":
                            LoadTemperatures(dataTypeDirectory, vcData);
                            break;
                        default:
                            break;
                    }
                }
                _data.Add(location.ToLower(), vcData);
            }
        }

        private void LoadTemperatures(string dataTypeDirectory, VisualCrossingData vcData)
        {
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
                    }
                }

            }
        }
    }
}
