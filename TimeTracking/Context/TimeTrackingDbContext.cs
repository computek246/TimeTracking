using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TimeTracking.Entities;

namespace TimeTracking.Context
{
    public class TimeTrackingDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkDays> WorkDays { get; set; }
        public DbSet<ActionsLog> ActionsLogs { get; set; }
        public DbSet<Setting> Settings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(@"Server=.;Database=TimeTrack;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        
    }
}
