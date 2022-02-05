using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracking.Domain.Entities
{
    public class WorkDaysMap : IEntityTypeConfiguration<WorkDays>
    {
        public void Configure(EntityTypeBuilder<WorkDays> builder)
        {
            builder.ToTable("WorkDays");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date).IsRequired().HasColumnType("date");
            builder.Property(x => x.StartAt).IsRequired().HasColumnType("time");
            builder.Property(x => x.EndAt).IsRequired().HasColumnType("time");
        }
    }
}