using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.BusinessContext;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [Route("")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private IApplicationRepository _repository;

        public TemperatureController(IApplicationRepository repository)
        {
            _repository = repository;
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

        [Route("/error")]
        public ActionResult<String> Error()
        {
            return @"{""Status"": ""ServerError""}";
        }
    }
}