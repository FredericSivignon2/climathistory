using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Weather.Application.Query;
using Microsoft.AspNetCore.Builder.Extensions;

namespace WeatherAPI.Routes
{
    public static class LocationRoutes
    {
        public static void MapLocationRoutes(this IEndpointRouteBuilder app)
        {
            app.MapGet("/{countryName}/alltowns", ([FromServices] ILocationInfo locInfo, string countryName) =>
            {
                return locInfo.GetAllLocationsByCountry(countryName);
            });

            app.MapGet("/allcountries", ([FromServices] ILocationInfo locInfo) =>
            {
                return locInfo.GetAllCountries();
            });
        }
    }
}
