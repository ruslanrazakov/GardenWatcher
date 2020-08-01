using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Data
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationContext _context;

        public ApplicationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<TemperatureSample> GetTemperatureSamples()
            => _context.TemperatureSamples.ToList();
        
        public void InsertTemperatureSample(TemperatureSample temperatureSample)
            => _context.TemperatureSamples.Add(temperatureSample);

        public IEnumerable<LightSample> GetLightSamples()
            => _context.LightSamples.ToList();
        
        public void InsertLightSample(LightSample lightSample)
            => _context.LightSamples.Add(lightSample);
        
        public IEnumerable<HumiditySample> GetHumiditySamples()
            => _context.HumiditySamples.ToList();

        public void InsertHumiditySample(HumiditySample humiditySample)
            => _context.HumiditySamples.Add(humiditySample);

        public void Save()
            => _context.SaveChanges();
        
    }
}
