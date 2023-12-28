using Weather.Application.Model;

namespace Weather.Application.Query
{
    public interface ILocationInfo
    {
        Task<IEnumerable<CountryModel>> GetAllCountries();
        IEnumerable<LocationModel> GetAllLocationsByCountry(long countryId);
    }
}
