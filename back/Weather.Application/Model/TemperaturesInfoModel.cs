
namespace Weather.Application.Model
{
    public record TemperaturesLocationInfoModel(string locationName, TemperaturesYearInfoModel[] YearsInfo)
    {
        public static TemperaturesLocationInfoModel Empty()
        {
            return new TemperaturesLocationInfoModel(string.Empty, []);
        }
    }
    
    public record TemperaturesYearInfoModel(int Year, TemperaturesInfoModel[] Days)
    {
        public static TemperaturesYearInfoModel Empty()
        {
            return new TemperaturesYearInfoModel(0, []);
        }
    }
    
    public record TemperaturesInfoModel(DateTime Date, decimal TempMax, decimal TempMin, decimal TempMean);
}
