﻿using System;
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
        IApplicationRepository _repository;
        ISensorsProcessor _sensorsProcessor;
        ILogger _logger;

        public ArduinoTrackingService(IApplicationRepository repository, ISensorsProcessor sensorsProcessor,
                                      ILogger <ArduinoTrackingService> logger)
        {
            _repository = repository;
            _sensorsProcessor = sensorsProcessor;
            _logger = logger;
            _logger.Log(LogLevel.Information, "ARDUINO TRACKING SERVICE STARTED");
        }

        public Task WriteMeasuresToDb()
        {
            _logger.Log(LogLevel.Information, "STARTING HANGFIRE BACKGROUND JOB....");

            MeasureModel measure = _sensorsProcessor.GetData();
            CheckSensorsResponseSuccess();
            _repository.InsertMeasure(measure);
            _repository.Save();
            return Task.Delay(10);
        }

        public MeasureModel GetCurrentMeasure()
        {
            return _sensorsProcessor.GetData();
        }

        private void CheckSensorsResponseSuccess()
        {
            if (!_sensorsProcessor.ConnectionSuccess)
                _logger.Log(LogLevel.Information, "ERROR DURING SENSORS WORKING. ACTIVE COM PORT NOT FOUND.");
        }
    }
}
