using System.Linq;
using Weather.Application.Model;

namespace Weather.Services.InMemory
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
}
