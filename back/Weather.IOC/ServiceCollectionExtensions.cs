
using Microsoft.Extensions.Configuration;
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
        public static IServiceCollection AddWeatherServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<IDbConnection>(sp => new NpgsqlConnection("Host=MagellanStore;Port=5432;Database=postgres;Username=postgres;Password=weaTHERapp_2024"));
            services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(configuration.GetConnectionString("WeatherDB")));
            services.AddTransient<IWeatherReader, WeatherServiceDatabase>();
            services.AddTransient<IWeatherRepository, WeatherRepository>();

            return services;
        }
    }
}

