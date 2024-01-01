namespace Weather.Database.Model
{
    public class MinMaxTemperaturesByYearData
    {
        public int Year {  get; set; }
        public decimal MinTemperature { get; set; }
        public decimal MaxTemperature { get; set; }
    }
}
