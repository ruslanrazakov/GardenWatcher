using System;
using System.Globalization;
using Server.Models;
using System.IO.Ports;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Server.BusinessContext;
using Microsoft.Extensions.Logging;

/// <summary>
/// Serial Port and arduino message parsing actions
/// </summary>
public class SensorsProcessor : ISensorsProcessor
{
    private int _id;
    private string _serialMessage;
    private string _portName;
    public bool ConnectionSuccess {get; set;}
    private ILogger _logger;

    public SensorsProcessor(ILogger<SensorsProcessor> logger)
    {
        _portName = String.Empty;
        _id = 0;
        _logger = logger;
    }

    public MeasureModel GetData()
    {
        string temperature = String.Empty, light = String.Empty, humidity = String.Empty;
        if(SerialPortOpen())
        {
            ConnectionSuccess = true;
            ParseSerialMessage(_serialMessage, out temperature, out light, out humidity);
        }
        return new MeasureModel() 
        {
            Id = _id++,
            Temperature = ConnectionSuccess ? Convert.ToInt32(temperature) : 0,
            Light = ConnectionSuccess ? Convert.ToInt32(light) : 0,
            Humidity = ConnectionSuccess ? Convert.ToInt32(humidity) : 0,
            PhotoFilePath = GetPhotoFilePath(),
            DateTime = DateTime.Now
        };
    }

    private bool SerialPortOpen()
    {
        foreach(var port in SerialPort.GetPortNames())
        {
            _portName = port;
        }
        
        if(_portName==String.Empty)
            return false;

        using (SerialPort serialPort = new SerialPort(_portName, 9600))
        {
            serialPort.Open();
            //Sometimes, first request to COM port returns part of serialMesssage
            //Second request is always OK
            //TODO: implement CRC algorithm instead of this
            _serialMessage = serialPort.ReadLine();
            _serialMessage = serialPort.ReadLine();
            _logger.Log(LogLevel.Information,  "SERIAL MESSAGE RECEIVED: " + _serialMessage);
            serialPort.Close();
        }
        return true;
    }

    private void ParseSerialMessage(string _serialMessage, out string temperature, out string light, out string humidity)
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

    private string GetPhotoFilePath()
    {
        return Directory.GetFiles("/home/rus/Photos").FirstOrDefault(f=>f.Contains(DateTime.Now.ToString("dd-HH")));
    }
}