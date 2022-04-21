using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Identity.Commands;
using CVBuilder.Application.Identity.Responses;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.User.Manager;
using MediatR;

namespace CVBuilder.Application.Identity.Handlers
{
    public class WebLoginHandler : IRequestHandler<WebLoginCommand, AuthenticationResult>
    {
        private readonly IAppUserManager _userManager;
        private readonly IIdentityService _identityService;

        public WebLoginHandler(
            IAppUserManager userManager,
            IIdentityService identityService)
        {
            _userManager = userManager;
            _identityService = identityService;
        }

        public async Task<AuthenticationResult> Handle(WebLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Incorrect password" }
                };
            }

            return await _identityService.GenerateAuthenticationResultAsync(user);
        }
    }
}