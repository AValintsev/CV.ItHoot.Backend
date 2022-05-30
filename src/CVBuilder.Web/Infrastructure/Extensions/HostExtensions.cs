using System;
using System.Threading.Tasks;
using CVBuilder.Application.User.Manager;
using CVBuilder.EFContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CVBuilder.Web.Infrastructure.Extensions
{
    public static class HostExtensions
    {
        public static async Task<IHost> EnsureDbExistsAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<EFDbContext>();
                var userManager = services.GetRequiredService<IAppUserManager>();
                await DbInitializer.Initialize(context);
                await BogusInitDb.Init(context,userManager);
            }
            catch (Exception)
            {
                // todo add logger
            }

            return host;
        }
    }
}
