using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Client.Models;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("http://localhost:5001");
            HttpResponseMessage response;
            try
            {
                response = await httpClient.GetAsync("api/Home");
            }
            catch(HttpRequestException)
            {
                return View("Error");
            }

            var measures = await response.Content.ReadAsAsync<IEnumerable<MeasureModel>>();
            if (response.IsSuccessStatusCode)
                return View(measures);
            else
                return View("Error");
        }
    }
}