using log4net;
using Microsoft.Extensions.Options;
using System.Reflection.PortableExecutable;

namespace VisualCrossingDataGrabber.Services
{
    internal class DataGrabber : IDataGrabber
    {
        private IVisualCrossingReader _visualCrossingReader;
        private GrabberSettings _settings;
        private ILog log = LogManager.GetLogger(typeof(DataGrabber));

        public DataGrabber(IVisualCrossingReader reader, IOptions<GrabberSettings> settings)
        {
            _visualCrossingReader = reader;
            _settings = settings.Value;
        }

        public async Task Run()
        {
            if (_settings.OverwriteExistingFiles)
            {
                Console.Write($"Overwrite existing files from {_settings.StartYear} to {_settings.EndYear}?");
                var key = Console.ReadKey();
                if (key.Key != ConsoleKey.Y)
                {
                    log.Warn("Operation aborted.");
                    return;
                }
            }

            try
            {
                foreach (string fullLocationName in Locations.All)
                {
                    log.Info($"Reading information for {fullLocationName}");

                    string locationName, countryName;
                    GetLocationAndCountryFromPath(fullLocationName, out locationName, out countryName);

                    string countryPath = Path.Combine(_settings.DataRootPath, char.ToUpper(countryName[0]) + countryName.Substring(1));
                    if (!Directory.Exists(countryPath))
                    {
                        log.Info($"Creating directory {countryPath}...");
                        Directory.CreateDirectory(countryPath);
                    }

                    // The location directory has its first character upper case
                    string locationPath = Path.Combine(countryPath, char.ToUpper(locationName[0]) + locationName.Substring(1));
                    if (!Directory.Exists(locationPath))
                    {
                        log.Info($"Creating directory {locationPath}...");
                        Directory.CreateDirectory(locationPath);
                    }
                    string temperaturePath = Path.Combine(locationPath, _settings.TemperatureDirName);
                    if (!Directory.Exists(temperaturePath))
                    {
                        log.Info($"Creating directory {temperaturePath}...");
                        Directory.CreateDirectory(temperaturePath);
                    }

                    int numberOfSkipped = 0;
                    for (int year = _settings.StartYear; year <= _settings.EndYear; year++)
                    {
                        string fileName = string.Format("temperatures_{0}_{1}.json", locationName.ToLower(), year);
                        string filePath = Path.Combine(temperaturePath, fileName);
                        if (File.Exists(filePath))
                        {
                            if (_settings.OverwriteExistingFiles)
                            {
                                log.Info($"Overwritting the file {fileName}...");
                                File.Delete(filePath);
                            }
                            else
                            {
                                numberOfSkipped++;
                                continue;
                            }
                        }
                        else
                        {
                            log.Info($"Creating the file {filePath}");
                        }
                        string result = await _visualCrossingReader.ReadLocationInfoPerYearAsync(year, fullLocationName);
                        if (result.Length > 0)
                        {
                            result = result.Insert(1, string.Format("\t\"year\": {0},\r\n", year));

                            File.WriteAllText(filePath, result);
                            log.Info("    -> File created.");
                        }
                    }
                    if (numberOfSkipped > 0)
                    {
                        log.Info($"{numberOfSkipped} files already exist. Skipping API calls...");
                    }
                }
            }
            catch (Exception e)
            {
                log.Fatal("A fatal error occured. Data grabbing aborted.", e);
            }
            DisplayStats();
        }

        private static void GetLocationAndCountryFromPath(string fullLocationName, out string locationName, out string countryName)
        {
            string[] tokens = fullLocationName.Split(',');
            if (tokens.Length != 2)
                throw new ArgumentException($"Invalid location name: ${fullLocationName}");

            locationName = tokens[0].Trim();
            countryName = tokens[1].Trim();
        }

        private static void DisplayStats()
        {

        }
    }
}
