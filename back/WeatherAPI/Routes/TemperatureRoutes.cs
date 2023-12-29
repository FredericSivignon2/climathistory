// TemperatureRoutes.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Weather.Application.Query;
using WeatherAPI;

public static class TemperatureRoutes
{
    private const string BaseRoute = "/api";

    public static void MapTemperatureRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGet("{BaseRoute}/all-countries", ([FromServices] IWeatherReader locInfo) =>
        {
            return locInfo.GetAllCountries();
        });
      
        app.MapGet("{BaseRoute}/{countryId}/all-locations", ([FromServices] IWeatherReader locInfo, long countryId) =>
        {
            return locInfo.GetAllLocationsByCountry(countryId);
        });

        app.MapGet("{BaseRoute}/location/{locationId}/temperatures/{year}", ([FromServices] IWeatherReader tempInfo, long locationId, int year) =>
        {
            return tempInfo.GetTemperaturesInfoFrom(locationId, year);
        });

        app.MapGet("{BaseRoute}/location/{locationId}/temperatures/average-per-year", ([FromServices] IWeatherReader tempInfo, long locationId) =>
        {
            return tempInfo.GetAvgTemperaturesForAllYears(locationId);
        });

        app.MapGet("{BaseRoute}/location/{locationId}/temperatures/minmax-per-year", ([FromServices] IWeatherReader tempInfo, long locationId) =>
        {
            return tempInfo.GetMinMaxTemperaturesDataFrom(locationId);
        });
    }
}
