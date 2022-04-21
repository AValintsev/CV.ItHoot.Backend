using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System.Threading.Tasks;
using CVBuilder.Web.Infrastructure.Extensions;

namespace CVBuilder.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = await CreateHostBuilder(args)
                .UseNLog()
                .Build()
                .EnsureDbExistsAsync();

            //start the program, a task will be completed when the host starts
            await host.StartAsync();

            //a task will be completed when shutdown is triggered
            await host.WaitForShutdownAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider(options =>
                {
                    //we don't validate the scopes, since at the app start and the initial configuration we need 
                    //to resolve some services (registered as "scoped") through the root container
                    options.ValidateScopes = false;
                    options.ValidateOnBuild = true;
                })
                .ConfigureWebHostDefaults(webBuilder => webBuilder
                    .ConfigureAppConfiguration(config =>
                    {
                        config.AddEnvironmentVariables();
                    })
                    .UseStartup<Startup>()).ConfigureLogging((_, logging) =>
                {
                    logging.AddNLog("nlog.config");
                    logging.AddNLogWeb("nlog.config");
                });

        
    }
}
