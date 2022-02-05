using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracking.Common.Constant;
using TimeTracking.Web.Security.Entities;

namespace TimeTracking.Web.Security.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entityBuilder)
        {
            var fields = typeof(AppValues.AppRoles).GetFields().ToList();
            var roles = fields
                .Select(e =>
                {
                    var id = fields.IndexOf(e) + 1;
                    var name = e.GetValue(e.Name)?.ToString() ?? "";
                    return new Role
                    {
                        Id = id,
                        Name = name,
                        NormalizedName = name.ToUpperInvariant(),
                    };
                });

            entityBuilder.HasData(roles.Where(e => !string.IsNullOrEmpty(e.Name)));

            // Each Role can have many entries in the UserRole join table
            entityBuilder.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // Each Role can have many associated RoleClaims
            entityBuilder.HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
        }
    }
}