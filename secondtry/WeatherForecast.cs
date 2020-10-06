using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace secondtry
{
   
    public class WeatherForecast
    {
        

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }
    }
}
