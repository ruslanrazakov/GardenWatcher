using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.BusinessContext;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LightController : Controller
    {
        private IApplicationRepository _repository;

        public LightController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<LightSample>> Get()
        {
            var lights = _repository.GetLightSamples();
            return lights.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<LightSample> GetById(int id)
        {
            return _repository.GetLightSamples().ToList().Find(l => l.Id == id);
        }
    }
}