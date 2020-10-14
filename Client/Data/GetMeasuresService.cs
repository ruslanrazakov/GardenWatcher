using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Client.Data
{
    public class GetMeasuresService
    {
         private IHttpClientFactory _httpClientFactory;

        public GetMeasuresService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<MeasureModel[]> GetMeasuresAsync(DateTime startDate)
        {
            IEnumerable<MeasureModel> measures;
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient();
                httpClient.BaseAddress = new Uri("http://185.43.6.193:80/");
                HttpResponseMessage response  = await httpClient.GetAsync("api/Home").ConfigureAwait(false);
                measures = await response.Content.ReadAsAsync<IEnumerable<MeasureModel>>().ConfigureAwait(false);
                measures = measures.Reverse();
            }
            catch(Exception)
            {
                measures = new List<MeasureModel>();
            }
            return measures.ToArray();
        }
    }
}
