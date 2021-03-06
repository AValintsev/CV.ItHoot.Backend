using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CVBuilder.Application.Identity.Responses;

namespace CVBuilder.Application.Identity.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> GenerateAuthenticationResultAsync(Models.User user);
        Task<IEnumerable<Claim>> GetUserClaims(Models.User user);
    }
}
