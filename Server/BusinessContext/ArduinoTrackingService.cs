using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Data;
using Server.Models;
using Hangfire;
using System.IO.Ports;
using Microsoft.Extensions.Logging;

namespace Server.BusinessContext
{
    /// <summary>
    /// Gets response from Arduino sensors and pushes information to DB
    /// </summary>
    public class ArduinoTrackingService : ITrackingService
    {
        ITemperatureSensor _temperatureSensor;
        ILightSensor _lightSensor;
        IHumiditySensor _humiditySensor;
        IApplicationRepository _repository;
        
        ILogger _logger;

        public ArduinoTrackingService(ITemperatureSensor temperatureSensor, ILightSensor lightSensor,
                                      IHumiditySensor sensor, IApplicationRepository repository,
                                      ILogger <ArduinoTrackingService> logger)
        {
            _temperatureSensor = temperatureSensor;
            _lightSensor = lightSensor;
            _humiditySensor = sensor;
            _repository = repository;
            _logger = logger;
            _logger.Log(LogLevel.Information, "ARDUINO TRACKING SERVICE STARTED");
        }

        public Task GetData()
        {
            _logger.Log(LogLevel.Information, "STARTING HANGFIRE BACKGROUND JOB....");

            TemperatureSample temperatureSample = _temperatureSensor.GetTemperatureMeasureFromArduino();
            LightSample lightSample = _lightSensor.GetLightMeasureFromArduino();
            HumiditySample humiditySample = _humiditySensor.GetHumidityMeasureFromArduino();

            if(!SensorsResponseSuccess(temperatureSample, lightSample, humiditySample))
                return Task.Delay(1000);
            else
            {
                _repository.InsertTemperatureSample(temperatureSample);
                _repository.InsertLightSample(lightSample);
                _repository.InsertHumiditySample(humiditySample);

                _repository.Save();
                return Task.Delay(1000);
            }
        }

        bool SensorsResponseSuccess(TemperatureSample temperatureSample, LightSample lightSample, HumiditySample humiditySample)
        {
            if (temperatureSample == null)
            {
                _logger.Log(LogLevel.Information, "ERROR DURING TEMPERATURE SENSOR WORKING. ACTIVE COM PORT NOT FOUND.");
                return false;
            }
            else if (lightSample == null )
            {
                 _logger.Log(LogLevel.Information, "ERROR DURING LIGHT SENSOR WORKING. ACTIVE COM PORT NOT FOUND.");
                return false;
            }
            else if (humiditySample == null)
            {
                 _logger.Log(LogLevel.Information, "ERROR DURING HUMIDITY SENSOR WORKING. ACTIVE COM PORT NOT FOUND.");
                return false;
            }
            else
                return true;
        }
    }
}
