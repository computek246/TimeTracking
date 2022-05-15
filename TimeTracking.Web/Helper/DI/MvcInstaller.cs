using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using TimeTracking.Common.Installers;
using TimeTracking.Common.Interfaces;
using TimeTracking.Web.Helper.Attributes;
using TimeTracking.Web.Helper.Implementations;

namespace TimeTracking.Web.Helper.DI
{
    public class MvcInstaller : IInstaller
    {
        public int Order => 3;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRazorPages();
            services.AddMvc(options =>
            {
                options.Filters.Add<ExceptionFilter>();
                options.EnableEndpointRouting = true;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddFluentValidation();

            services.AddControllersWithViews()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddTransient(typeof(ICurrentUserService<>), typeof(CurrentUserService<>));
            services.AddHttpContextAccessor();

            services.AddKendo();
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleWares(env);
        }
    }
}
