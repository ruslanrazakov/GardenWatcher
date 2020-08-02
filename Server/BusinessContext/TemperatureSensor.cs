using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Globalization;
using Microsoft.Extensions.Logging;

namespace Server.BusinessContext
{
    public class TemperatureSensor : Sensor, ITemperatureSensor
    {

        public TemperatureSample GetTemperatureMeasureFromArduino()
        {
            foreach(var port in SerialPort.GetPortNames())
            {
                _portName = port;
            }

            if(_portName==String.Empty)
                return null;

            using (SerialPort serialPort = new SerialPort(_portName, 9600))
            {
                serialPort.Open();
                Thread.Sleep(1000);
                //Sometimes, first request to COM port returns part of serialMesssage
                //Second request is always OK
                //TODO: realize CRC algorithm instead of this
                serialMessage = serialPort.ReadLine();
                serialMessage = serialPort.ReadLine();
                System.Diagnostics.Debug.WriteLine(serialMessage + " ***SERIAL MESSAGE");
                serialPort.Close();
            }
            return new TemperatureSample() { Id = _id++, Temperature = ParseSerialMessage(serialMessage, prefix: 't'), DateTime = DateTime.Now };
        }
    }
}
