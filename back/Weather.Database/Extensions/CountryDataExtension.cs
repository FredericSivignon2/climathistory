using Weather.Application.Model;
using Weather.Database.Model;

namespace Weather.Database.Extensions
{
    public static class CountryDataExtension
    {
        public static IEnumerable<CountryModel> MapToCountryModels(this IEnumerable<CountryData> countries)
        {
            return countries.Select(country => new CountryModel(country.CountryId, country.Name));
        }
    }
}
