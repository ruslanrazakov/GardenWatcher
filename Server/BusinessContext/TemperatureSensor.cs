using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.BusinessContext
{
    public class TemperatureSensor : ITemperatureSensor
    {
        string arduinoAdress = "http://192.168.1.20";
        int i = 0;
        public TemperatureSample GetTemperatureMeasureFromArduino()
        {
            return new TemperatureSample() { Id = i++, Temperature = 25f, DateTime = DateTime.Now};
        }
    }
}
