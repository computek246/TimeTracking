using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracking
{
    public class WorkDays
    {
        public int Id { get; set; }
        public int? StatusId { get; set; }
        public string DayName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? StartAt { get; set; }
        public TimeSpan? EndAt { get; set; }
        public TimeSpan? BaseHour { get; set; }
        public TimeSpan? TotalHour { get; set; }
    }

    public class WorkDaysMap : IEntityTypeConfiguration<WorkDays>
    {
        public void Configure(EntityTypeBuilder<WorkDays> builder)
        {
            builder.ToTable("WorkDays");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date).IsRequired().HasColumnType("date");
            builder.Property(x => x.DayName).IsRequired().HasMaxLength(15);
        }
    }
}
