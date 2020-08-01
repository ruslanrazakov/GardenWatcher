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
    public class ArduinoTrackingService
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

            StartService();
        }

        public void StartService() => RecurringJob.AddOrUpdate(() => GetDataFromArduino(), Cron.Minutely);

        public void GetDataFromArduino()
        {
            _logger.Log(LogLevel.Information, "STARTING HANGFIRE BACKGROUND JOB....");
            TemperatureSample temperatureSample = _temperatureSensor.GetTemperatureMeasureFromArduino();
            LightSample lightSample = _lightSensor.GetLightMeasureFromArduino();
            HumiditySample humiditySample = _humiditySensor.GetHumidityMeasureFromArduino();
            if (temperatureSample == null || lightSample == null || humiditySample == null)
            {
                _logger.Log(LogLevel.Information, "ERROR DURING SENSOR SERVISES WORKING. NO ACTIVE COM PORT NOT FOUND.");
                return;
            }
            _repository.InsertTemperatureSample(temperatureSample);
            _repository.InsertLightSample(lightSample);
            _repository.InsertHumiditySample(humiditySample);
            _repository.Save();
        }
    }
}
