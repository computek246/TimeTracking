using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Serilog;

namespace TimeTracking.Web
{
    public class Program
    {

        public static async Task Main(string[] args) =>
            await CreateHostBuilder(args).Build().RunAsync();


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog((hostingContext, loggerConfig) =>
                {
                    loggerConfig.ReadFrom.Configuration(hostingContext.Configuration);
                });
    }
}
