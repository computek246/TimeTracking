using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracking.Common;
using TimeTracking.Common.Constant;

namespace TimeTracking.Domain.Entities
{
    public class SettingMap : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("Setting");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(400);
            builder.Property(x => x.Value).IsRequired().HasMaxLength(2000);
            builder.HasIndex(x => x.Name).HasDatabaseName("IX_Setting_Name");

            builder.HasData(new List<Setting>
            {
                new()
                {
                    Id = 1,
                    Name = SettingValues.GeneralSetting.LegalCopyright,
                    Value = "Copyright © {0} Dell Inc. or its subsidiaries. All Rights Reserved."
                }
            });
        }
    }
}