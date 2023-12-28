using System.Linq;
using Weather.Application.Model;

namespace Weather.Services.VisualCrossing
{
    internal static class VisualCrossingTemperatureDataExtension
    {
        public static YearInfoModel MapToYearInfoModel(this VisualCrossingTemperatureData data)
        {
            return new YearInfoModel(data.Year,
                data.Days.Select(dayInfo => new DayInfoModel(
                    dayInfo.Datetime, 
                    dayInfo.Tempmax, 
                    dayInfo.Tempmin, 
                    dayInfo.Temp)).ToArray());
        }
    }

    internal static class VisualCrossingDataExtension
    {
        public static LocationInfoModel MapToLocationInfoModel(this VisualCrossingData data)
        {
            return new LocationInfoModel(data.Town, data.Temperatures.Select(vctd => vctd.MapToYearInfoModel()).ToArray());
        }
    }
}
