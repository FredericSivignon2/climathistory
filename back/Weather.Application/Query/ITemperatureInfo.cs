using Weather.Application.Model;
using Weather.Application.Models;
using Weather.Services.InMemory;

namespace Weather.Application.Query
{
    public interface ITemperatureInfo
    {
        YearInfoModel GetTemperaturesDataFrom(string countryName, string townName, int year);
        IEnumerable<MeanPerYearModel> GetAverageTemperaturesDataFrom(string countryName, string locationName);
        IEnumerable<MinMaxPerYearModel> GetMinMaxTemperaturesDataFrom(string countryName, string locationName);
    }
}
