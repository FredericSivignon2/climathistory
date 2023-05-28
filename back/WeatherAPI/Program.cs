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
WeartherData data = new WeartherData();
data.Load();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("MyCorsPolicy");
//app.UseAuthorization();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});
app.MapGet("/{locationName}/temperatures/{year}", (string locationName, int year) =>
{
    return data.GetTemperaturesDataFrom(locationName, year);
});

app.Run();
