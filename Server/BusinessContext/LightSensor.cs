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
    public class LightSensor : ILightSensor
    {
        char _delimeter;
        int _id;
        string serialMessage;
        string portName;

        public LightSensor()
        {
            _delimeter = ';';
            _id = 0;
        }

        public LightSample GetLightMeasureFromArduino()
        {
            foreach(var port in SerialPort.GetPortNames())
            {
                portName = port;
            }

            using (SerialPort serialPort = new SerialPort(portName, 9600))
            {
                serialPort.Open();
                Thread.Sleep(1000);
                serialMessage = serialPort.ReadLine();
                serialMessage = serialPort.ReadLine();
                System.Diagnostics.Debug.WriteLine(serialMessage + " ***SERIAL MESSAGE");
                serialPort.Close();
            }
            return new LightSample() { Id = _id++, LightLevel = ParseSerialMessage(serialMessage), DateTime = DateTime.Now };
        }


        float ParseSerialMessage(string serialMessage)
        {
            string light = String.Empty;
            bool isLight = false;
            for (int i = 0; i < serialMessage.Length; i++)
            {
                if (serialMessage[i] == 'l')
                {
                    isLight = true;
                    continue;
                }
                if (serialMessage[i] == _delimeter || serialMessage[i] == 't') isLight = false;

                if (isLight) light += serialMessage[i];
            }
            float result;
            return float.TryParse(light, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out result) ? result : 0.0f;
        }
    }
    
}
