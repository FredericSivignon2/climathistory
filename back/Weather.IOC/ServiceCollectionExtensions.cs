
using Microsoft.Extensions.DependencyInjection;
using Weather.Application.Query;
using Weather.Database;
using Weather.Database.Postgres;
using Weather.Services.InMemory;

namespace Weather.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeatherServices(this IServiceCollection services)
        {
            services.AddDataServices();
            //services.AddTransient<ITemperatureInfo, WeatherServiceInMemory>(); 
            //services.AddTransient<ILocationInfo, WeatherServiceInMemory>();
            services.AddTransient<ITemperatureInfo, WeatherServiceDatabase>();
            services.AddTransient<ILocationInfo, WeatherServiceDatabase>();

            services.AddSingleton<IWeatherRepository, WeatherRepository>();

            return services;
        }
    }
}

