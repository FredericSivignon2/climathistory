using System.Linq;
using Weather.Application.VisualCrossing.Model;

namespace Weather.Services.VisualCrossing
{
    internal static class VisualCrossingFileDataExtension
    {
        public static VisualCrossingLocationModel MapToVisualCrossingLocationModel(this VisualCrossingLocationFileData data, string locationName)
        {
            var temperaturesPerYar = new Dictionary<int, VisualCrossingDayTemperatureModel[]>();
            foreach (VisualCrossingTemperatureFileData temperatureFileData in data.Temperatures)
            {
                temperaturesPerYar.Add(temperatureFileData.Year, temperatureFileData?.Days?.MapToVisualCrossingDayModelArray() ?? []);
            }
            return new VisualCrossingLocationModel(
                locationName,
                data.Temperatures.Select(temperatureData =>
                    new VisualCrossingYearTemperatureModel(
                        temperatureData.Year, 
                        temperatureData?.Days?.MapToVisualCrossingDayModelArray().ToArray() ?? [])).ToArray());
        }
    }

    internal static class VisualCrossingDaysFileDataExtension
    {
        public static VisualCrossingDayTemperatureModel[] MapToVisualCrossingDayModelArray(this IEnumerable<VisualCrossingDaysFileData> data)
        {
            return data.Select(x => new VisualCrossingDayTemperatureModel(x.Datetime, x.Tempmax, x.Tempmin, x.Temp)).ToArray();
        }
    }

    //internal static class VisualCrossingDataExtension
    //{
    //    public static IEnumerable<VisualCrossingLocationModel> MapToLocationInfoModel(this Dictionary<string, VisualCrossingFileData> data)
    //    {
    //        return data.Select(locationData => locationData.Value.MapToVisualCrossingLocationModel(locationData.Key));
    //    }
    //}
}
