using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DatabaseFeeder.Services;
using Weather.Database;
using Weather.Database.Postgres;
using Weather.Application.VisualCrossing.Queries;
using Weather.Services.VisualCrossing;
using Npgsql;
using System.Data;
using Microsoft.Extensions.Configuration;
using DatabaseFeeder;

Console.WriteLine("Starting...");

var app = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<ImportSettings>(hostContext.Configuration.GetSection("ImportSettings")); 
        services.AddFileDataServices();
        services.AddTransient<IDbConnection>(sp => new NpgsqlConnection("Host=magellanstore;Port=5555;Database=postgres;Username=postgres;Password=weaTHERapp_2024"));
        services.AddTransient<IVisualCrossingReader, WeatherServiceVisualCrossing>();
        services.AddTransient<IWeatherRepository, WeatherRepository>();
        services.AddTransient<IFeederService, FeederService>();
    }).Build();

//var configuration = app.Services.GetRequiredService<IConfiguration>();
//var settings = configuration.GetSection("ImportSettings").Get<ImportSettings>();

var service = app.Services.GetRequiredService<IFeederService>();
await service.Feed();