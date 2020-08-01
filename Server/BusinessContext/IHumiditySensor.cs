using Server.Models;

namespace Server.BusinessContext
{
    public interface IHumiditySensor
    {
        HumiditySample GetHumidityMeasureFromArduino();
    }
}
