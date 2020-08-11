using Server.Models;

namespace Server.BusinessContext
{
    public interface ISensorsProcessor
    {
        MeasureModel GetData();
    }
}
