using Weather.Application.Model;

namespace Weather.Application.Query
{
    public interface ILocationInfo
    {
        IEnumerable<CountryModel> GetAllCountries();
        IEnumerable<LocationModel> GetAllLocationsByCountry(string countryName);
    }
}
