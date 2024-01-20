using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Weather.Database.Model
{
    public class TemperatureData
    {
        public decimal Value {  get; set; }

        public override string ToString()
        {
            return $"{Value} °C";
        }
    }
}
