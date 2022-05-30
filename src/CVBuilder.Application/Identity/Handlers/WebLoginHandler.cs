using System.Threading;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CVBuilder.Application.Core.Exceptions;
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
                throw new ForbiddenException("User does not exist");
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!userHasValidPassword)
            {
                throw new ForbiddenException("Incorrect password");
            }

            return await _identityService.GenerateAuthenticationResultAsync(user);
        }
    }
}