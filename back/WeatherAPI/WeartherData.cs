using System.Text.Json;
using System;
using System.Text;

namespace WeatherAPI
{
    public class WeartherData
    {
        private const string sourceDirectory = "D:\\Development\\Web\\React\\climathistorydata";
        private List<VisualCrossingData> data = new();

        public WeartherData()
        {
        }

        public IEnumerable<VisualCrossingData> Data
        {
            get { return data; }
        }

        public string GetTemperaturesDataFrom(string locationName, int year)
        {
            Console.WriteLine($"Reading temperatures for {locationName} in {year}");

            VisualCrossingData? vcd = data.Find(currentData =>
            {
                return currentData.Location.ToLower() == locationName.ToLower();
            });
            if (vcd == null) { return string.Empty; }


            //JsonSerializerOptions options = new JsonSerializerOptions();
            var temperatures = vcd.Temperatures.First<VisualCrossingTemperatureData>(tdata =>
            {
                return tdata.Year == year;
            });
            return JsonSerializer.Serialize<VisualCrossingTemperatureData>(temperatures);
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
                data.Add(vcData);
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
