using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracking.Domain.Entities
{
    public class ProjectMap : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasQueryFilter(e => e.IsActive);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProjectName).IsRequired().HasMaxLength(400);
            builder.HasIndex(x => x.ProjectName).HasDatabaseName("IX_Setting_Name");

            builder.HasMany(e => e.ActionsLogs).WithOne(e => e.Project).HasForeignKey(e => e.ProjectId);

            builder.HasData(new List<Project>
            {
                new() {Id = 1, ProjectName = "Infinity", IsActive = true},
                new() {Id = 2, ProjectName = "EVO", IsActive = true},
                new() {Id = 3, ProjectName = "Emerald", IsActive = true},
                new() {Id = 4, ProjectName = "SELECT.TSM", IsActive = true},
                new() {Id = 5, ProjectName = "Other", IsActive = true}
            });
        }
    }
}