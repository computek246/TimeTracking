using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeTracking.Web.Security.Entities;

namespace TimeTracking.Web.Security.Context
{
    public class ApplicationDbContext
        : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        ////
        //// Summary:
        ////     Gets or sets the Microsoft.EntityFrameworkCore.DbSet`1 of Users.
        //public virtual DbSet<TUser> Users { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the Microsoft.EntityFrameworkCore.DbSet`1 of User claims.
        //public virtual DbSet<TUserClaim> UserClaims { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the Microsoft.EntityFrameworkCore.DbSet`1 of User logins.
        //public virtual DbSet<TUserLogin> UserLogins { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the Microsoft.EntityFrameworkCore.DbSet`1 of User tokens.
        //public virtual DbSet<TUserToken> UserTokens { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the Microsoft.EntityFrameworkCore.DbSet`1 of User roles.
        //public virtual DbSet<TUserRole> UserRoles { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the Microsoft.EntityFrameworkCore.DbSet`1 of roles.
        //public virtual DbSet<TRole> Roles { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the Microsoft.EntityFrameworkCore.DbSet`1 of role claims.
        //public virtual DbSet<TRoleClaim> RoleClaims { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // To Set MaxLength for all string Properties
            foreach (var property in builder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
            {
                // skip property that have MaxLength
                if (property.GetMaxLength() == null)
                {
                    property.SetMaxLength(256);
                }
            }
        }
    }
}