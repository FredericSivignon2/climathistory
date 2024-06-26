﻿namespace Weather.Database.Model
{
    public class TemperaturesData
    {
        public long LocationId { get; set; }
        public DateTime Date { get; set; }
        public decimal MinTemperature { get; set; }
        public decimal MaxTemperature { get; set; }
        public decimal AvgTemperature { get; set; }

        public override string ToString()
        {
            return $"{Date} - min {MinTemperature}, max {MaxTemperature}, average {AvgTemperature} (location id {LocationId})";
        }
    }
}
