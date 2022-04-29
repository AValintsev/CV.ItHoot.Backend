using CVBuilder.Application.Identity.Commands;
using CVBuilder.Application.Identity.Responses;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.User.Manager;
using MediatR;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;

namespace CVBuilder.Application.Identity.Handlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthenticationResult>
    {
        private readonly IAppUserManager _userManager;
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public RefreshTokenHandler(
            IAppUserManager userManager,
            IIdentityService identityService,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public async Task<AuthenticationResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var validatedToken = _tokenService.GetPrincipalFromToken(request.Token);
            if (validatedToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
            }

            var expiryDate = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value;
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(long.Parse(expiryDate));
            if (expiryDateTimeUtc > DateTime.UtcNow && !request.ForceRefresh)
            {
                return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };
            }

            var userId = validatedToken.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var storedRefreshToken = await _tokenService.GetRefreshTokenAsync(int.Parse(userId), request.RefreshToken);
            if (storedRefreshToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not exist" } };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryAt)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };
            }

            if (storedRefreshToken.IsRevoked)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has been revoked" } };
            }

            if (storedRefreshToken.IsUsed)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has been used" } };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not match this JWT" } };
            }

            storedRefreshToken.IsUsed = true;
            await _tokenService.UpdateRefreshTokenAsync(storedRefreshToken);
            var user = await _userManager.FindByIdAsync(userId);
            return await _identityService.GenerateAuthenticationResultAsync(user);
        }
    }
}