using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class HumiditySample
    {
        public int Id { get; set; }

        public float HumidityLevel { get; set; }

        public DateTime DateTime { get; set; }
    }
}
