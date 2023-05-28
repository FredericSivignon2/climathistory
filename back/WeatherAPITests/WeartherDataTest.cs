using System.Xml.Serialization;
using VisualCrossingDataGrabber;
using WeatherAPI;

namespace WeatherAPITests
{
    public class WeartherDataTest
    {
        [Fact]
        public void WeartherData_EnsureInitialization()
        {
            WeartherData data = new ();
            data.Load();

            Assert.NotNull(data.Data);
            Assert.NotEmpty(data.Data);
            Assert.True(data.Data.Count() == 31); // One town right now
            Assert.NotEmpty(data.Data.ElementAt(0).Temperatures);
            Assert.True(data.Data.ElementAt(0).Temperatures.Count() >= 50); // 50 years 
        }
    }
}