using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DatabaseFeeder.Services;
using Weather.Database;
using Weather.Database.Postgres;
using Weather.Application.Query;
using Weather.Application.VisualCrossing.Queries;
using Weather.Services.VisualCrossing;
using Npgsql;
using System.Data;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Starting...");

var app = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddFileDataServices();
        services.AddTransient<IDbConnection>(sp => new NpgsqlConnection("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=weaTHERapp_2024"));
        services.AddTransient<IVisualCrossingReader, WeatherServiceVisualCrossing>();
        services.AddTransient<IWeatherRepository, WeatherRepository>();
        services.AddTransient<IFeederService, FeederService>();
    }).Build();

var configuration = app.Services.GetRequiredService<IConfiguration>();
var service = app.Services.GetRequiredService<IFeederService>();
await service.Feed();