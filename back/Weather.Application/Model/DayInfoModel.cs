
namespace Weather.Application.Model
{

    public record DayInfoModel(DateTime Date, decimal TempMax, decimal TempMin, decimal TempMean);

    public record YearInfoModel(int Year, DayInfoModel[] Days)
    {
        public static YearInfoModel Empty()
        {
            return new YearInfoModel(0, []);
        }
    }
    
    public record LocationInfoModel(string locationName, YearInfoModel[] YearsInfo)
    {
        public static LocationInfoModel Empty()
        {
            return new LocationInfoModel(string.Empty, []);
        }
    }
}
