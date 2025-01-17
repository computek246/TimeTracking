﻿using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TimeTracking.Domain.Entities;

namespace TimeTracking.Domain.Context
{
    public class TimeTrackingDbContext : DbContext
    {
        public TimeTrackingDbContext()
        {
        }

        public TimeTrackingDbContext(DbContextOptions<TimeTrackingDbContext> options)
            : base(options)
        {
        }


        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkDays> WorkDays { get; set; }
        public DbSet<ActionsLog> ActionsLogs { get; set; }
        public DbSet<Setting> Settings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=TimeTrack;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // To Set MaxLength for all string Properties
            foreach (var property in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
            {
                // skip property that have MaxLength
                if (property.GetMaxLength() == null)
                {
                    property.SetMaxLength(256);
                }
            }
        }


    }
}
