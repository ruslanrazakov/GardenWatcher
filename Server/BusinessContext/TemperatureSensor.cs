using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Server.BusinessContext
{
    public class TemperatureSensor : ITemperatureSensor
    {
        string _delimeter;
        int _id;
        string serialMessage;

        public TemperatureSensor()
        {
            _delimeter = ";";
            _id = 0;
        }

        int i = 0;
        public TemperatureSample GetTemperatureMeasureFromArduino()
        {
            return new TemperatureSample() { Id = _id++, Temperature = 25f, DateTime = DateTime.Now};
        }
    }
}
