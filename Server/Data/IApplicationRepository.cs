using Server.Models;
using System.Collections.Generic;

namespace Server.Data
{
    public interface IApplicationRepository
    {
        IEnumerable<TemperatureSample> GetTemperatureSamples();
        void InsertTemperatureSample(TemperatureSample temperatureSample);
        IEnumerable<LightSample> GetLightSamples();
        void InsertLightSample(LightSample lightSample);
        void Save();
    }
}