using WeatherAPI;
using Weather.IoC;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureBaseServices();
builder.Services.AddWeatherServices();

var app = builder.Build();
// Define value via ASPNETCORE_URLS
//app.Urls.Add("https://localhost:4000");

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("MyCorsPolicy");

app.MapTemperatureRoutes();

app.Run();
