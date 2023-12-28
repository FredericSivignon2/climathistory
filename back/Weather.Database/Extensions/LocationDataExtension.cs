using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Application.Model;
using Weather.Database.Model;

namespace Weather.Database.Extensions
{
    public static class LocationDataExtension
    {
        public static IEnumerable<LocationModel> MapToLocationModels(this IEnumerable<LocationData> locations)
        {
            return locations.Select(location => new LocationModel(location.LocationId, location.Name, location.CountryId));
        }
    }
}
