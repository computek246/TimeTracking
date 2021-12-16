using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace TimeTracking
{
    public class TimeTrackingDbContext : DbContext
    {
        public TimeTrackingDbContext()
        {
            
        }


        public DbSet<WorkDays> WorkDays { get; set; }
        public DbSet<ActionsLog> ActionsLogs { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=TimeTrack;Trusted_Connection=True;MultipleActiveResultSets=true;",
                builder =>
                {
                    builder.CommandTimeout(90000);
                    builder.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
