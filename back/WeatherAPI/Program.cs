using WeatherAPI;
using Weather.IoC;
using Weather.Database;

Console.WriteLine("Starting the application...");
var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureBaseServices();

Console.WriteLine("Reading appsettings.json...");
var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

builder.Services.AddWeatherServices(configuration);

var app = builder.Build();
// Define value via ASPNETCORE_URLS
//app.Urls.Add("https://localhost:4000");

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("MyCorsPolicy");

app.MapTemperatureRoutes();

var scope = app.Services.CreateScope();
var repository = scope.ServiceProvider.GetRequiredService<IWeatherRepository>();
if (await repository.IntializeSchema())
{
    Console.WriteLine("Databaase schema already exists.");
}
else
{
    Console.WriteLine("Database schema was not exist and has been created.");
}
Console.WriteLine($"Number of countries records: {await repository.GetCountriesCountAsync()}");
Console.WriteLine($"Number of locations records: {await repository.GetLocationsCountAsync()}");
Console.WriteLine($"Number of temperatures records: {await repository.GetTemperaturesCountAsync()}");

app.Run();
