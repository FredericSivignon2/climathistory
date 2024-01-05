namespace Weather.Services.VisualCrossing
{
    /// <summary>
    /// Provides methods to read meteo data from files repository (where data are stored in json files)
    /// </summary>
    public interface IFileDataStore
    {
        IEnumerable<string> GetAllCountriesNames();
        IEnumerable<string> GetAllLocationsNames(string countryName);
        VisualCrossingLocationFileData? GetDataPerLocation(string countryName, string locationName);
        VisualCrossingLocationFileData[] GetDataPerCountry(string country);
    }
}
