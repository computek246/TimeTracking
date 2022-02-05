using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeTracking.Common;
using TimeTracking.Common.Installers;
using TimeTracking.Web.Data;
using TimeTracking.Web.Security.Context;
using TimeTracking.Web.Security.Entities;
using TimeTracking.Web.Security.Implementations;

namespace TimeTracking.Web.Security.DI
{
    public class SecurityInstaller : IInstaller
    {
        public int Order => 2;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<User, Role>()
                .AddClaimsPrincipalFactory<AdditionalUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            });

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<IdentityOptions>(configuration.GetSection("IdentityOptions"));
            services.AddScoped<IEmailSender, EmailSender>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
