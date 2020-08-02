using Server.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Globalization;


namespace Server.BusinessContext
{
    public class HumiditySensor : Sensor, IHumiditySensor
    { 
        public HumiditySample GetHumidityMeasureFromArduino()
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
                //Sometimes, first request to COM port returns part of serialMesssage
                //Second request is always OK
                //TODO: realize CRC algorithm instead of this
                serialMessage = serialPort.ReadLine();
                serialMessage = serialPort.ReadLine();
                System.Diagnostics.Debug.WriteLine(serialMessage + " ***SERIAL MESSAGE");
                serialPort.Close();
            }
            
            return new HumiditySample() { Id = _id++, HumidityLevel = ParseSerialMessage(serialMessage, prefix : 'h'), DateTime = DateTime.Now };
        }
    }
}