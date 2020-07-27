using Server.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.BusinessContext
{
    public class LightSensor : ILightSensor
    {
        int id = 0;
        public LightSample GetLightMeasureFromArduino()
        {
            return new LightSample() { Id = id++, LightLevel = 2500, DateTime = DateTime.Now };
        }
    }
}
