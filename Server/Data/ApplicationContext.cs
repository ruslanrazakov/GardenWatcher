using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<LightSample> LightSamples { get; set; }
        public DbSet<TemperatureSample> TemperatureSamples { get; set; }
        public DbSet<HumiditySample> HumiditySamples { get; set; }
        public DbSet<PhotoSample> PhotoSamples { get; set; }

    }
}
