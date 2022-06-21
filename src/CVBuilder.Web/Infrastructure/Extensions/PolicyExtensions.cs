using CVBuilder.Web.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using CVBuilder.Models;

namespace CVBuilder.Web.Infrastructure.Extensions
{
    public static class PolicyExtensions
    {
        public static void AddPolicyJwtRole(this AuthorizationOptions options, string name, Enums.RoleTypes role)
        {
            options.AddPolicyRole(name, JwtBearerDefaults.AuthenticationScheme, role);
        }

        public static void AddPolicyJwtAnyRole(this AuthorizationOptions options, string name,
            params Enums.RoleTypes[] roles)
        {
            options.AddPolicy(name, x =>
            {
                x.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                x.RequireAssertion(p => roles.Any(role => p.User.IsInRole(role.ToString())));
            });
        }

        public static void AddPolicyAccessTokenRole(this AuthorizationOptions options, string name,
            Enums.RoleTypes role)
        {
            options.AddPolicyRole(name, AccessTokenDefaults.AuthenticationScheme, role);
        }

        private static void AddPolicyRole(this AuthorizationOptions options, string name, string scheme,
            Enums.RoleTypes role)
        {
            options.AddPolicy(name, x =>
            {
                x.AuthenticationSchemes.Add(scheme);
                x.RequireRole(role.ToString());
            });
        }
    }
}