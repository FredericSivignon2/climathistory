namespace VisualCrossingDataGrabber.Services
{
    internal interface IVisualCrossingReader
    {
        Task<string> ReadLocationInfoPerYearAsync(int year, string townName);
    }
}