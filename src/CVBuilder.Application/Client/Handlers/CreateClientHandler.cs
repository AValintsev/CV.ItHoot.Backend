using AutoMapper;
using CVBuilder.Application.Client.Commands;
using CVBuilder.Application.Client.Responses;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.User.Manager;
using CVBuilder.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace CVBuilder.Application.Client.Handlers
{

    internal class CreateClientHandler : IRequestHandler<CreateClientCommand, ClientResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppUserManager _userManager;
        private readonly IShortUrlService _shortUrlService;

        public CreateClientHandler(
            IMapper mapper,
            IAppUserManager userManager,
            IShortUrlService shortUrlService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _shortUrlService = shortUrlService;
        }

        public async Task<ClientResponse> Handle(CreateClientCommand command, CancellationToken cancellationToken)
        {
            var user = new Models.User
            {
                UserName = command.Email,
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = command.PhoneNumber,
                Site = command.Site,
                Contacts = command.Contacts,
                CompanyName = command.CompanyName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            user.ShortUrl = new ShortUrl()
            {
                Url = _shortUrlService.GenerateShortUrl()
            };

            var result = await _userManager.CreateAsync(user, user.ShortUrl.Url);

            if (!result.Succeeded)
            {
                var message = string.Join(Environment.NewLine, result.Errors.Select(x => x.Description));
                throw new ValidationException(message);
            }

            var addRole = await _userManager.AddToRolesAsync(user, new List<string>()
            {
                Enums.RoleTypes.Client.ToString(),
            });
            if (!addRole.Succeeded)
            {
                var message = string.Join(Environment.NewLine, addRole.Errors.Select(x => x.Description));
                throw new ValidationException(message);
            }

            return _mapper.Map<ClientResponse>(user);
        }
    }
}