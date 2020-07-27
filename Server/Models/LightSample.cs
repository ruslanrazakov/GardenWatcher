using Server.BusinessContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Server.Models
{
    public class LightSample
    {
        public int Id { get; set; }

        public float LightLevel { get; set; }

        public DateTime DateTime { get; set; }
    }
}
