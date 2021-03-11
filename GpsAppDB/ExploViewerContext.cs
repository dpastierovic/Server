using GpsAppDB.Entities;
using Microsoft.EntityFrameworkCore;

namespace GpsAppDB
{
    public class ExploViewer : DbContext
    {
        public const int SRID = 4326;

        public ExploViewer(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>();
            modelBuilder.Entity<Activity>();
            modelBuilder.Entity<Marker>();
        }

        public DbSet<Athlete> Athletes { get; set; }

        public  DbSet<Activity> Activities { get; set; }

        public DbSet<Marker> Markers { get; set; }
    }
}