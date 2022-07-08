using CVBuilder.Application.Caching;
using CVBuilder.Application.Caching.Interfaces;
using CVBuilder.Application.Core.Infrastructure.Interfaces;
using CVBuilder.Application.Core.Settings;
using CVBuilder.Application.Pipelines;
using CVBuilder.Application.Resume.Services;
using CVBuilder.Application.Resume.Services.DocxBuilder;
using CVBuilder.Application.Resume.Services.Interfaces;
using CVBuilder.Repository;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Application.Core.Infrastructure
{
    public class AddDependenciesRegister : IDependencyRegistrar
    {
        public int Order => 1;

        public void Register(IServiceCollection services, ITypeFinder typeFinder, AppSettings appSettings)
        {
            services.AddScoped<ICVBuilderFileProvider, CVBuilderFileProvider>();

            services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));
            services.AddScoped(typeof(IDeletableRepository<,>), typeof(EFDeletableRepository<,>));

            services.AddSingleton<ICacheKeyService, CacheKeyService>();
            services.AddSingleton<ILocker, MemoryCacheManager>();
            services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
            services.AddHttpClient();
            services.AddScoped<IImageService, GyazoImageService>();
            services.AddScoped<IPdfPrinter, PdfPrinterBrowser>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipe<,>));

            var assembly = typeof(AddDependenciesRegister).Assembly;
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(assembly);

            // Add DocxBuilder services
            services.AddSingleton<IDocxBuilder, DocxBuilder>();
        }
    }
}