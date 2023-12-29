using System.Xml.Linq;

namespace Weather.Application.VisualCrossing.Model
{
    public record VisualCrossingLocationModel(string Name, VisualCrossingYearTemperatureModel[] TemperaturesPerYear)
    {
        public override string ToString()
        {
            return $"{Name} - {TemperaturesPerYear.Length} years of temperatures info.";
        }
    }

    public record VisualCrossingYearTemperatureModel(int Year, VisualCrossingDayTemperatureModel[] TemperaturesPerDay)
    {
        public override string ToString()
        {
            return $"{Year} - {TemperaturesPerDay.Length} days of temperatures info.";
        }
    }

    public record VisualCrossingDayTemperatureModel(DateTime Date, decimal TemperatureMax, decimal TemperatureMin, decimal TemperatureAverage)
    {
        public override string ToString()
        {
            return $"{Date}, max {TemperatureMax}, min {TemperatureMin}, avg {TemperatureAverage}";
        }
    }
}
