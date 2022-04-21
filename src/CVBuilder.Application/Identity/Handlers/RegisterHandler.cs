using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Identity.Commands;
using CVBuilder.Application.Identity.Responses;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.User.Manager;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Identity.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly IAppUserManager _userManager;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;


        public RegisterHandler(
            IAppUserManager userManager,
            IIdentityService identityService,
            IMapper mapper)
        {
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existedUser = await _userManager.FindByEmailAsync(request.Email);
            if (existedUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User already exist" }
                };
            }

            var user = new Models.Entities.User
            {
                Email = request.Email,
                UserName = request.Email,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var createdUser = await _userManager.CreateAsync(user, request.Password);
            if (!createdUser.Succeeded)
            {
                var message = string.Join(Environment.NewLine, createdUser.Errors.Select(x => x.Description));
                throw new Exception(message);
            }


            var addRole = await _userManager.AddToRolesAsync(user, new List<string>()
            {
                Enums.RoleTypes.User.ToString(),
            });
            if (!addRole.Succeeded)
            {
                var message = string.Join(Environment.NewLine, addRole.Errors.Select(x => x.Description));
                throw new Exception(message);
            }

            return await _identityService.GenerateAuthenticationResultAsync(user);
        }
    }
}