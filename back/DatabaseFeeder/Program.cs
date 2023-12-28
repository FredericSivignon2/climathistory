using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DatabaseFeeder.Services;
using Weather.Database;
using Weather.Database.Postgres;
using Weather.Application.Query;
using Weather.Services.InMemory;
using Npgsql;
using System.Data;
using Microsoft.Extensions.Configuration;

var app = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDataServices();
        services.AddTransient<IDbConnection>(sp => new NpgsqlConnection("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=weaTHERapp_2024"));
        services.AddTransient<ITemperatureInfo, WeatherServiceInMemory>();
        services.AddTransient<ILocationInfo, WeatherServiceInMemory>();
        services.AddTransient<IWeatherRepository, WeatherRepository>();
        services.AddTransient<IFeederService, FeederService>();
    }).Build();

var configuration = app.Services.GetRequiredService<IConfiguration>();
var service = app.Services.GetRequiredService<IFeederService>();
await service.Feed();