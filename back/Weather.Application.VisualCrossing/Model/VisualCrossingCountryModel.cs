namespace Weather.Application.VisualCrossing.Model
{
    public record VisualCrossingCountryModel(string Name)
    {
        public override string ToString() 
        {
            return $"{Name}";
        }
    }
}
