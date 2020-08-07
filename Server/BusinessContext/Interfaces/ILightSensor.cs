using Server.Models;

namespace Server.BusinessContext
{
    public interface ILightSensor
    {
        LightSample GetLightMeasureFromArduino();
    }
}
