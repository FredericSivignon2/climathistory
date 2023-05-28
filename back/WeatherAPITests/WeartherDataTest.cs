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
            WeartherService data = new ();
            data.Load();

            Assert.NotNull(data.Data);
            Assert.NotEmpty(data.Data);
            Assert.True(data.Data.Count() == 31); // One town right now
            
            VisualCrossingData paris = data.Data["paris"];
            Assert.True(paris.Temperatures.Count() > 0);
        }
    }
}