using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumidityController : Controller
    {
        private IApplicationRepository _repository;

        public HumidityController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<HumiditySample>> Get()
        {
            var humidities = _repository.GetHumiditySamples();
            return humidities.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<HumiditySample> GetById(int id)
        {
            return _repository.GetHumiditySamples().ToList().Find(h => h.Id == id);
        }
    }
}