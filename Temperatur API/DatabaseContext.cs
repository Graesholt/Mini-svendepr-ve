using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Temperatur_API.Models;

namespace Temperatur_API
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Temperature> Temperatures { get; set; }

        public DbSet<Moisture> Moistures { get; set; }

        public DbSet<Room> Rooms { get; set; }
    }
}
