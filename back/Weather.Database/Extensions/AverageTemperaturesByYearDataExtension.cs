using Weather.Application.Models;
using Weather.Database.Model;

namespace Weather.Database.Extensions
{
    public static class AverageTemperaturesByYearDataExtension
    {
        public static IEnumerable<AveragePerYearModel> MapToAveragePerYearModels(this IEnumerable<AverageTemperaturesByYearData> data)
        {
            return data.Select(atbyd => new AveragePerYearModel(
                atbyd.Year, 
                atbyd.AverageOfAvg, 
                atbyd.AverageOfMax, 
                atbyd.AverageOfMin));
        }
    }
}
