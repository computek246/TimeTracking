using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracking
{
    public class Setting
    {
        public Setting() { }

        public Setting(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

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
                    Value = $"Copyright © {DateTime.Now.Year} Dell Inc. or its subsidiaries. All Rights Reserved."
                }
            });
        }
    }
}
