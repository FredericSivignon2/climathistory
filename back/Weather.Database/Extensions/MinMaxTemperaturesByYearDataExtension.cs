using Weather.Database.Model;
using Weather.Services.VisualCrossing;

namespace Weather.Database.Extensions
{
    public static class MinMaxTemperaturesByYearDataExtension
    {
        public static IEnumerable<MinMaxPerYearModel> MapToMinMaxPerYearModels(this IEnumerable<MinMaxTemperaturesByYearData> data)
        {
            return data.Select(mmtbyd => new MinMaxPerYearModel(
                mmtbyd.Year,
                mmtbyd.Min,
                mmtbyd.Max));
        }
    }
}
