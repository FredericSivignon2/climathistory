namespace Weather.Database.Model
{
    public class LocationData
    {
        public long LocationId { get; set; }
        public required string Name { get; set; }
        public long CountryId { get; set; }
    }
}
