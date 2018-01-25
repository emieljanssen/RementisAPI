using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RementisApi.Models
{
    public class SensordataContext : DbContext
    {
        public SensordataContext(DbContextOptions<SensordataContext> options)
            : base(options)
        {
        }

        public DbSet<SensorItem> SensorItems { get; set; }
        public DbSet<Agendadata> Agendadata { get; set; }
        public DbSet<Profile> Profile { get; set; }

    }
}
