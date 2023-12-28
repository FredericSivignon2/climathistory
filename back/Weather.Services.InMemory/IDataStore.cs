namespace Weather.Services.InMemory
{
    public interface IDataStore
    {
        IEnumerable<string> GetAllCountriesNames();
        Dictionary<string, VisualCrossingData> GetDataPerCountry(string country);
    }
}
