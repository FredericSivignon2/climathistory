using Weather.Application.VisualCrossing.Model;

namespace Weather.Application.VisualCrossing.Queries
{
    public interface IVisualCrossingLocationInfo
    {
        Task<IEnumerable<VisualCrossingCountryModel>> GetAllCountries();
        IEnumerable<VisualCrossingLocationModel> GetAllLocationsByCountry(long countryId);
    }
}
