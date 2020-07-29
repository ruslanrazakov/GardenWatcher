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
    public class TemperatureController : ControllerBase
    {
        private IApplicationRepository _repository;

        private ArduinoTrackingService _trackingService;

        public TemperatureController(IApplicationRepository repository, ArduinoTrackingService trackingService)
        {
            _repository = repository;
            _trackingService = trackingService;
        }

        [HttpGet]
        public ActionResult<List<TemperatureSample>> Get()
        {
            var temperatures = _repository.GetTemperatureSamples();
            return temperatures.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<TemperatureSample> GetById(int id)
        {
            return _repository.GetTemperatureSamples().ToList().Find(t => t.Id == id);
        }
    }
}