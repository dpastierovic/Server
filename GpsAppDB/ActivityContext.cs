using System;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace GpsAppDB
{
    public class ActivityContext : DbContext
    {
        public ActivityContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().HasData(new Activity
            {
                Id = 1,
                Name = "seed activity",
                Timestamp = DateTime.Now,
                Gear = "CUBE Agree C62 Race Disc",
                StartingPoint = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326).CreatePoint(new Coordinate(48, 18))
        });
        }

        public DbSet<Activity> Activities { get; set; }
    }
}