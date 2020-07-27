using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class TemperatureSample
    {
        public int Id { get; set; }

        public float Temperature { get; set; }

        public DateTime DateTime { get; set; }
    }
}
