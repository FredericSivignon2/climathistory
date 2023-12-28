using WeatherAPI;
using Weather.IoC;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Routes;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureBaseServices();
builder.Services.AddWeatherServices();

var app = builder.Build();
app.Urls.Add("https://localhost:4000");
//WeartherService data = new WeartherService();
//data.Load();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("MyCorsPolicy");
//app.UseAuthorization();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});


app.MapTemperatureRoutes();
app.MapLocationRoutes();

app.Run();
