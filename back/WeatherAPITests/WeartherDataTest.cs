using System.Xml.Serialization;
using VisualCrossingDataGrabber;
using WeatherAPI;

namespace WeatherAPITests
{
    public class WeartherDataTest
    {
        private const int COUNTRY_NUMBER = 1;

        [Fact]
        public void WeartherData_EnsureInitialization()
        {
            //WeartherService data = new ();
            //data.Load();

            //Assert.NotNull(data.Data);
            //Assert.NotEmpty(data.Data);
            //Assert.True(data.Data.Count() == COUNTRY_NUMBER);

            //Dictionary<string, VisualCrossingData> franceData = data.Data["france"];
            //VisualCrossingData paris = franceData["paris"];
            //Assert.True(paris.Temperatures.Count() > 0);
        }
    }
}