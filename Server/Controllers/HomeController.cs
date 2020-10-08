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
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IApplicationRepository _repository;
        private ITrackingService _trackingService;

        public HomeController(IApplicationRepository repository, ITrackingService trackingService)
        {
            _repository = repository;
            _trackingService = trackingService;
        }

        [HttpGet]
        public ActionResult<List<MeasureModel>> Get()
        {
            var measures = _repository.GetMeasures();
            return measures.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<MeasureModel> GetById(int id)
        {
            return _repository.GetMeasures().ToList().Find(t => t.Id == id);
        }

        [Route("/getcurrent")]
        public ActionResult<MeasureModel> GetCurrentMeasures()
        {
            return _trackingService.GetCurrentMeasure();
        }

        [Route("/error")]
        public ActionResult<String> Error()
        {
            return @"{""Status"": ""ServerError""}";
        }
    }
}