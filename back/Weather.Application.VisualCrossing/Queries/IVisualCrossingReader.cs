using Weather.Application.VisualCrossing.Model;

namespace Weather.Application.VisualCrossing.Queries
{
    public interface IVisualCrossingReader
    {
        Task<IEnumerable<VisualCrossingCountryModel>> GetAllCountries();
        IEnumerable<VisualCrossingLocationModel> GetAllLocationsByCountry(string countryName);
    }
}
