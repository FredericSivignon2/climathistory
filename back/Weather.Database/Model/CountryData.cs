namespace Weather.Database.Model
{
    public class CountryData
    {
        public long CountryId { get; set; }
        public required string Name { get; set; }

        public override string ToString()
        {
            return $"{Name} ({CountryId})";
        }
    }

}
