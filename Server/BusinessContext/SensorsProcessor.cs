using System;
using System.Globalization;
using Server.Models;
using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;
using Server.BusinessContext;

/// <summary>
/// Contains common fields and methods for all sensors in business layer
/// </summary>
public class SensorsProcessor : ISensorsProcessor
{
    protected int _id;
    protected string _serialMessage;
    protected string _portName;

    public SensorsProcessor()
    {
        _portName = String.Empty;
        _id = 0;
    }

    public MeasureModel GetData()
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
            //TODO: implement CRC algorithm instead of this
            _serialMessage = serialPort.ReadLine();
            _serialMessage = serialPort.ReadLine();
            System.Diagnostics.Debug.WriteLine(_serialMessage + "   SERIAL MESSAGE");
            serialPort.Close();
        }

        string temperature, light, humidity;

        ParseSerialMessage(_serialMessage, out temperature, out light, out humidity);

        return new MeasureModel() 
        {
            Id = _id++,
            Temperature = Convert.ToInt32(temperature),
            Light = Convert.ToInt32(light),
            Humidity = Convert.ToInt32(humidity),
            PhotoFilePath = String.Empty,
            DateTime = DateTime.Now
        };
    }

    protected void ParseSerialMessage(string _serialMessage, out string temperature, out string light, out string humidity)
    {
        bool tempIsParsing = false, lightIsParsing = false, humidityIsParsing = false;

        temperature = String.Empty;
        light = String.Empty;
        humidity = String.Empty;

        for(int i = 0; i < _serialMessage.Length; i++)
        {
            if(_serialMessage[i] == 't')
            {
                tempIsParsing = true;
                lightIsParsing = false;
                humidityIsParsing = false;
                continue;
            }
            else if(_serialMessage[i] == 'l')
            {
                lightIsParsing = true;
                tempIsParsing = false;
                humidityIsParsing = false;
                continue;
            }
            else if(_serialMessage[i] == 'h')
            {
                humidityIsParsing = true;
                lightIsParsing = false;
                tempIsParsing = false;
                continue;
            }

            if(tempIsParsing)
                temperature += _serialMessage[i];
            else if(lightIsParsing)
                light += _serialMessage[i];
            else if(humidityIsParsing)
                humidity += _serialMessage[i];
        }
    }
}