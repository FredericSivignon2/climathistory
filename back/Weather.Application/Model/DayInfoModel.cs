
namespace Weather.Application.Model
{

    public record DayInfoModel(DateTime Date, double TempMax, double TempMin, double TempMean);

    public record YearInfoModel(int Year, DayInfoModel[] Days);
    
    public record LocationInfoModel(string locationName, YearInfoModel[] YearsInfo);
}
