using Weather.Application.Model;
using Weather.Database.Model;

namespace Weather.Database.Extensions
{
    public static class TemperatureDataExtension
    {
        public static TemperatureModel? MapToTemperatureModel(this TemperatureData? data)
        {
            if (data == null)
            {
                return null;
            }

            return new TemperatureModel(data.Value);
        }
    }
}
