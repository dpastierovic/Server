using Microsoft.EntityFrameworkCore;

namespace GpsAppDB
{
    public class AthleteContext : DbContext
    {
        public AthleteContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>();
        }

        public DbSet<Athlete> Athletes { get; set; }
    }
}