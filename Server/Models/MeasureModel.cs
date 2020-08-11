using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class MeasureModel
    {
        public int Id { get; set; }
        public int Temperature { get; set; }
        public int Light { get; set; }
        public int Humidity { get; set; }
        public string PhotoFilePath { get; set; }
        public DateTime DateTime { get; set; }
    }
}