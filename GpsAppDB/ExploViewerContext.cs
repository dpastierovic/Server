﻿using GpsAppDB.Entities;
using Microsoft.EntityFrameworkCore;

namespace GpsAppDB
{
    public class ExploViewer : DbContext
    {
        public ExploViewer(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>();
            modelBuilder.Entity<Activity>();
        }

        public DbSet<Athlete> Athletes { get; set; }

        public  DbSet<Activity> Activities { get; set; }
    }
}