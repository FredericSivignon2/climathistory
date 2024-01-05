namespace VisualCrossingDataGrabber
{
    internal class GrabberSettings
    {
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public required string DataRootPath { get; set; }
        public required string TemperatureDirName { get; set;}
        public bool OverwriteExistingFiles { get; set; }
        public required string VisualCrossingKey { get; set; }
    }
}
