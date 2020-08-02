using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Contains common fields and methods for all sensors in business layer
/// </summary>
public abstract class Sensor
{
    protected char _delimeter;
    protected int _id;
    protected string serialMessage;
    protected string _portName;

    public Sensor()
    {
        _portName = String.Empty;
        _delimeter = ';';
        _id = 0;
    }

    /// <summary>
    /// Types of sensor:
    /// 't' - temperature sensor
    /// 'l' - light sensor
    /// 'h' - humidity sensor
    /// </summary>
    /// <param name="serialMessage">Whole serial message from COM port</param>
    /// <param name="prefix">Indicates type of sensor information to parse</param>
    /// <returns></returns>
    protected float ParseSerialMessage(string serialMessage, char prefix)
    {
        string measure = String.Empty;
        bool isParsing = false;
        for (int i = 0; i < serialMessage.Length; i++)
        {
            if (serialMessage[i] == prefix)
            {
                isParsing = true;
                continue;
            }
            else if(!Char.IsDigit(serialMessage[i]) && !Char.IsPunctuation(serialMessage[i]))
                isParsing = false;

            if (isParsing) measure += serialMessage[i];
        }
        float result;
        return float.TryParse(measure, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out result) ? result : 0.0f;
    }
}