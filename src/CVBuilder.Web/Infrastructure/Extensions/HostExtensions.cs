using System;
using System.Threading.Tasks;
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
                await DbInitializer.Initialize(context);
            }
            catch (Exception)
            {
                // todo add logger
            }

            return host;
        }
    }
}
