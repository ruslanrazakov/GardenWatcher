using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Data;
using Server.Models;
using Hangfire;

namespace Server.BusinessContext
{
    public class ArduinoTrackingService
    {
        ITemperatureSensor _temperatureSensor;
        ILightSensor _lightSensor;
        IApplicationRepository _repository;

        public ArduinoTrackingService(ITemperatureSensor temperatureSensor, ILightSensor lightSensor, IApplicationRepository repository)
        {
            _temperatureSensor = temperatureSensor;
            _lightSensor = lightSensor;
            _repository = repository;
        }

        public void StartService() => BackgroundJob.Enqueue(() => InitConnection());

        public void InitConnection()
        {
            System.Diagnostics.Debug.WriteLine("STARTING HANGFIRE BACKGROUND JOB....");
            TemperatureSample temperatureSample = _temperatureSensor.GetTemperatureMeasureFromArduino();
            LightSample lightSample = _lightSensor.GetLightMeasureFromArduino();
            if (temperatureSample == null || lightSample == null)
                return;
            _repository.InsertTemperatureSample(temperatureSample);
            _repository.InsertLightSample(lightSample);
            _repository.Save();
        }
    }
}
