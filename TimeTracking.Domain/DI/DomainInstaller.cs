using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeTracking.Common.Installers;
using TimeTracking.Domain.Context;

namespace TimeTracking.Domain.DI
{
    public class DomainInstaller : IInstaller
    {
        public int Order => 1;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<TimeTrackingDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

    }
}
