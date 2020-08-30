using Server.Models;

namespace Server.BusinessContext
{
    public interface ISensorsProcessor
    {
        bool ConnectionSuccess {get; set;}
        MeasureModel GetData();
    }
}
