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

        public void Save()
            => _context.SaveChanges();

        public IEnumerable<MeasureModel> GetMeasures()
            => _context.Measures.ToList();

        public void InsertMeasure(MeasureModel measure)
            => _context.Measures.Add(measure);
    }
}
