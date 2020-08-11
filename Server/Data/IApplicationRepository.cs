using Server.Models;
using System.Collections.Generic;

namespace Server.Data
{
    public interface IApplicationRepository
    {
        IEnumerable<MeasureModel> GetMeasures();
        void InsertMeasure(MeasureModel measure);
        void Save();
    }
}