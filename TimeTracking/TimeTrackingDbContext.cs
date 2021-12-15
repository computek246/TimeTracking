using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace TimeTracking
{
    public class TimeTrackingDbContext : DbContext
    {
        public DbSet<WorkDays> WorkDays { get; set; }
        public DbSet<ActionsLog> ActionsLogs { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=TimeTrack;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
