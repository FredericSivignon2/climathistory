namespace Weather.Database.Model
{
    public record AverageTemperaturesByYearData(int Year, decimal AverageOfAvg, decimal AverageOfMax, decimal AverageOfMin)
    {
        public override string ToString()
        {
            return $"{Year}, average of average {AverageOfAvg}";
        }
    }
}
