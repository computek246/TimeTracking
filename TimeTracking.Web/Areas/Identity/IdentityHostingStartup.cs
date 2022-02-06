using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TimeTracking.Web.Areas.Identity.IdentityHostingStartup))]
namespace TimeTracking.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}