
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;
using Weather.Application.Query;
using Weather.Database;
using Weather.Database.Postgres;
using Weather.Services.VisualCrossing;

namespace Weather.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeatherServices(this IServiceCollection services)
        {
            services.AddTransient<IDbConnection>(sp => new NpgsqlConnection("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=weaTHERapp_2024"));
            services.AddTransient<IWeatherReader, WeatherServiceDatabase>();
            services.AddSingleton<IWeatherRepository, WeatherRepository>();

            return services;
        }
    }
}

