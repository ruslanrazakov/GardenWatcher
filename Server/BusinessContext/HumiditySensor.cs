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
    public class HumiditySensor : IHumiditySensor
    { 
        char _delimeter;
        int _id;
        string serialMessage;
        string portName;

        public HumiditySensor()
        {
            _delimeter = ';';
            _id = 0;
        }

        public HumiditySample GetHumidityMeasureFromArduino()
        {
            foreach(var port in SerialPort.GetPortNames())
            {
                portName = port;
            }

            if(portName==String.Empty)
                return null;

            using (SerialPort serialPort = new SerialPort(portName, 9600))
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
            
            return new HumiditySample() { Id = _id++, HumidityLevel = ParseSerialMessage(serialMessage), DateTime = DateTime.Now };
        }

        float ParseSerialMessage(string serialMessage)
        {
            string humidity = String.Empty;
            bool isHumidity = false;
            for (int i = 0; i < serialMessage.Length; i++)
            {
                if (serialMessage[i] == 'l')
                {
                    isHumidity = true;
                    continue;
                }
                if (serialMessage[i] == _delimeter || serialMessage[i] == 't') isHumidity = false;

                if (isHumidity) humidity += serialMessage[i];
            }
            float result;
            return float.TryParse(humidity, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out result) ? result : 0.0f;
        }
    }
}