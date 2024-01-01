using Microsoft.AspNetCore.Mvc;
using Weather.Application.Query;

namespace WeatherAPI
{
    public static class TemperatureRoutes
    {
        private const string BaseRoute = "/api/v1.0";

        public static void MapTemperatureRoutes(this IEndpointRouteBuilder app)
        {
            app.MapGet($"{BaseRoute}/country/all", async ([FromServices] IWeatherReader locInfo) =>
            {
                return await locInfo.GetAllCountries();
            });

            app.MapGet($"{BaseRoute}/country/{{countryId:long}}/all-locations", async ([FromServices] IWeatherReader locInfo, long countryId) =>
            {
                return await locInfo.GetAllLocationsByCountry(countryId);
            });

            app.MapGet($"{BaseRoute}/location/{{locationId:long}}/temperatures/{{year:int}}", async ([FromServices] IWeatherReader tempInfo, long locationId, int year) =>
            {
                return await tempInfo.GetTemperaturesInfoFrom(locationId, year);
            });

            app.MapGet($"{BaseRoute}/location/{{locationId:long}}/temperatures/average-per-year", async ([FromServices] IWeatherReader tempInfo, long locationId) =>
            {
                return await tempInfo.GetAvgTemperaturesForAllYears(locationId);
            });

            app.MapGet($"{BaseRoute}/location/{{locationId:long}}/temperatures/minmax-per-year", async ([FromServices] IWeatherReader tempInfo, long locationId) =>
            {
                return await tempInfo.GetMinMaxTemperaturesDataFrom(locationId);
            });
        }
    }
}
