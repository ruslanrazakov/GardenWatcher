using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Data;
using Server.Models;
using Hangfire;
using System.IO.Ports;

namespace Server.BusinessContext
{
    public class ArduinoTrackingService
    {
        ITemperatureSensor _temperatureSensor;
        ILightSensor _lightSensor;
        IHumiditySensor _humiditySensor;
        IApplicationRepository _repository;

        public ArduinoTrackingService(ITemperatureSensor temperatureSensor, ILightSensor lightSensor,
                                      IHumiditySensor sensor, IApplicationRepository repository)
        {
            _temperatureSensor = temperatureSensor;
            _lightSensor = lightSensor;
            _humiditySensor = sensor;
            _repository = repository;
            
            StartService();
        }

        public void StartService() => RecurringJob.AddOrUpdate(() => GetDataFromArduino(), Cron.Minutely);

        public void GetDataFromArduino()
        {
            System.Diagnostics.Debug.WriteLine("STARTING HANGFIRE BACKGROUND JOB....");
            TemperatureSample temperatureSample = _temperatureSensor.GetTemperatureMeasureFromArduino();
            LightSample lightSample = _lightSensor.GetLightMeasureFromArduino();
            HumiditySample humiditySample = _humiditySensor.GetHumidityMeasureFromArduino();
            if (temperatureSample == null || lightSample == null)
                return;
            _repository.InsertTemperatureSample(temperatureSample);
            _repository.InsertLightSample(lightSample);
            _repository.Save();
        }
    }
}
