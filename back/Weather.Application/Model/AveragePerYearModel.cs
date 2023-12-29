using System.Text.Json.Serialization;

namespace Weather.Application.Models
{
    public record AveragePerYearModel(int Year, decimal AverageOfAverage, decimal AverageOfMax, decimal AverageOfMin);
}
