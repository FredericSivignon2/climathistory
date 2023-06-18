using WeatherAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyCorsPolicy",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});

var app = builder.Build();
app.Urls.Add("https://localhost:4000");
WeartherService data = new WeartherService();
data.Load();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("MyCorsPolicy");
//app.UseAuthorization();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});
app.MapGet("/{countryName}/{townName}/temperatures/{year}", (string countryName, string townName, int year) =>
{
    return data.GetTemperaturesDataFrom(countryName, townName, year);
});

app.MapGet("/{countryName}/{townName}/average-temperatures-per-year", (string countryName, string townName) =>
{
    return data.GetAverageTemperaturesDataFrom(countryName, townName);
});

app.MapGet("/{countryName}/{townName}/minmax-temperatures-per-year", (string countryName, string townName) =>
{
    return data.GetMinMaxTemperaturesDataFrom(countryName, townName);
});

app.MapGet("/{countryName}/alltowns", (string countryName) =>
{
    return data.GetAllLocationsByCountry(countryName);
});

app.MapGet("/allcountries", () =>
{
    return data.GetAllCountries();
});

app.Run();
