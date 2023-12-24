using System.Text.Json.Serialization;

namespace Weather.Application.Models
{
    public record MeanPerYearModel(int Year, double Mean, double MeanMax, double MeanMin);
}
