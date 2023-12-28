using System.Text.Json.Serialization;

namespace Weather.Application.Models
{
    public record MeanPerYearModel(int Year, decimal Mean, decimal MeanMax, decimal MeanMin);
}
