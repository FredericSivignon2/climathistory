using Weather.Application.Model;
using Weather.Database.Model;

namespace Weather.Database.Extensions
{
    public static class TemperaturesDataExtension
    {
        public static TemperaturesYearInfoModel MapToYearInfoModel(this IEnumerable<TemperaturesData> data, int year)
        {
            return new TemperaturesYearInfoModel(
                year, 
                data.Select(td => new TemperaturesInfoModel(
                    td.Date, 
                    td.MaxTemperature, 
                    td.MinTemperature, 
                    td.AvgTemperature)).ToArray());
        }
    }
}
