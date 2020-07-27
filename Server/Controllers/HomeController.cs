using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.BusinessContext;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IApplicationRepository _repository;
        private ITemperatureSensor _temperatureSensor;
        private ILightSensor _lightSensor;

        private ArduinoTrackingService _trackingService;

        public HomeController(IApplicationRepository repository)
        {
            _repository = repository;
            _trackingService = new ArduinoTrackingService(_temperatureSensor, _lightSensor, _repository);
            _trackingService.StartService();
        }

        [HttpGet]
        public ActionResult<List<TemperatureSample>> Get()
        {
            var temperatures = _repository.GetTemperatureSamples();
            return temperatures.ToList();
        }
    }
}