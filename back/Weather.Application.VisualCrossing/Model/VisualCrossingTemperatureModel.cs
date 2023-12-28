namespace Weather.Application.VisualCrossing.Model
{
    public class VisualCrossingTemperatureModel
    {
        public required Dictionary<int, VisualCrossingDayModel[]> TemperaturesPerYear { get; set;}
    }
}
