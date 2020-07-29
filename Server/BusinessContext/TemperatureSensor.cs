using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Globalization;

namespace Server.BusinessContext
{
    public class TemperatureSensor : ITemperatureSensor
    {
        char _delimeter;
        int _id;
        string serialMessage;

        public TemperatureSensor()
        {
            _delimeter = ';';
            _id = 0;
        }

        public TemperatureSample GetTemperatureMeasureFromArduino()
        {
            using (SerialPort serialPort = new SerialPort("COM3", 9600))
            {
                serialPort.Open();
                Thread.Sleep(1000);
                serialMessage = serialPort.ReadExisting();
                System.Diagnostics.Debug.WriteLine(serialMessage + " SERIAL MESSAGE");
                serialPort.Close();
            }
            return new TemperatureSample() { Id = _id++, Temperature = ParseSerialMessage(serialMessage), DateTime = DateTime.Now };
        }

        float ParseSerialMessage(string serialMessage)
        {
            string temperature = String.Empty;
            bool isTemperature = false;
            for (int i = 0; i < serialMessage.Length; i++)
            {
                if (serialMessage[i] == 't')
                {
                    isTemperature = true;
                    continue;
                }
                if (serialMessage[i] == _delimeter || serialMessage[i] == 'l') isTemperature = false;

                if (isTemperature) temperature += serialMessage[i];
            }
            float result;
            return float.TryParse(temperature, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out result) ? result : 0.0f;
        }
    }
}
