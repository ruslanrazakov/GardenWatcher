using System.Threading.Tasks;
using Server.Models;

namespace Server.BusinessContext
{
    public interface ITrackingService
    {
        /// <summary>
        /// Pushes data from sensors to DB
        /// </summary>
        /// <returns></returns>
        Task WriteMeasuresToDb();

        /// <summary>
        /// Gets current measures from serial
        /// </summary>
        /// <returns></returns>
        MeasureModel GetCurrentMeasure();
    }
}