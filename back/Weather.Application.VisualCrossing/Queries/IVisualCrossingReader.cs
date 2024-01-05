using Weather.Application.VisualCrossing.Model;

namespace Weather.Application.VisualCrossing.Queries
{
    public interface IVisualCrossingReader
    {
        Task<IEnumerable<VisualCrossingCountryModel>> GetAllCountries();
        IEnumerable<string> GetAllLocationsNames(string countryName);
        VisualCrossingLocationModel? GetLocationByCountry(string countryName, string locationName);
        IEnumerable<VisualCrossingLocationModel> GetAllLocationsByCountry(string countryName);
    }
}
