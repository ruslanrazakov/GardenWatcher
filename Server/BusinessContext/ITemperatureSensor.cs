using Server.Models;

namespace Server.BusinessContext
{
    public interface ITemperatureSensor
    {
        TemperatureSample GetTemperatureMeasureFromArduino();
    }
}