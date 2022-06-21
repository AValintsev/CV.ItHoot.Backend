using CVBuilder.Application.Identity.Commands;
using CVBuilder.Application.Identity.Responses;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.User.Manager;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CVBuilder.Application.Identity.Handlers
{
    public class GetCurrentUserByTokenHandler : IRequestHandler<GetCurrentUserByTokenCommand, AuthenticationResult>
    {
        private readonly IAppUserManager _userManager;
        private readonly IIdentityService _identityService;

        public GetCurrentUserByTokenHandler(
            IIdentityService identityService,
            IAppUserManager userManager)
        {
            _identityService = identityService;
            _userManager = userManager;
        }

        public async Task<AuthenticationResult> Handle(GetCurrentUserByTokenCommand command,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            return await _identityService.GenerateAuthenticationResultAsync(user);
        }
    }
}