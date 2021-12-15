using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracking
{
    public class ActionsLog
    {
        public int Id { get; set; }
        public int? ActionId { get; set; }
        public string ActionName { get; set; }
        public DateTime ActionDate { get; set; }
    }

    public class ActionsLogMap : IEntityTypeConfiguration<ActionsLog>
    {
        public void Configure(EntityTypeBuilder<ActionsLog> builder)
        {
            builder.ToTable("ActionsLogs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ActionDate).IsRequired();
            builder.Property(x => x.ActionName).IsRequired().HasMaxLength(50);
        }
    }
}
