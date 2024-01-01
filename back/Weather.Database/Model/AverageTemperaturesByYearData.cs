namespace Weather.Database.Model
{
    public class AverageTemperaturesByYearData
    {
        public int Year { get; set; }
        public decimal AverageOfAvg { get; set; } 
        public decimal AverageOfMax { get; set; } 
        public decimal AverageOfMin { get; set; }

        public override string ToString()
        {
            return $"{Year}, average of average {AverageOfAvg}";
        }
    }
}
