using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CVBuilder.Application.Core.Infrastructure;
using CVBuilder.Application.Core.Settings;
using CVBuilder.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace CVBuilder.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }

        public static void UseSVBuilderSwagger(this IApplicationBuilder application)
        {
            var swaggerSettings = application.ApplicationServices.GetService<IOptions<SwaggerSettings>>().Value;
            
            application.UseSwagger(options => options.RouteTemplate = swaggerSettings.JsonRoute);
            
            application.UseSwaggerUI(options =>
                options.SwaggerEndpoint(swaggerSettings.UIEndpoint, swaggerSettings.Description));
        }
    }
}