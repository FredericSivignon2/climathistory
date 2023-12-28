// TemperatureRoutes.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Weather.Application.Query;
using WeatherAPI;

public static class TemperatureRoutes
{
    public static void MapTemperatureRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{countryName}/{townName}/temperatures/{year}", ([FromServices] ITemperatureInfo tempInfo, string countryName, string townName, int year) =>
        {
            return tempInfo.GetTemperaturesInfoFrom(countryName, townName, year);
        });

        app.MapGet("/{countryName}/{townName}/average-temperatures-per-year", ([FromServices] ITemperatureInfo tempInfo, string countryName, string townName) =>
        {
            return tempInfo.GetAverageTemperaturesDataFrom(countryName, townName);
        });

        app.MapGet("/{countryName}/{townName}/minmax-temperatures-per-year", ([FromServices] ITemperatureInfo tempInfo, string countryName, string townName) =>
        {
            return tempInfo.GetMinMaxTemperaturesDataFrom(countryName, townName);
        });
    }
}
