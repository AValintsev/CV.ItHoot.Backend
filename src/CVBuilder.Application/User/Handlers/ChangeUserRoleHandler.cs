using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.User.Commands;
using CVBuilder.Application.User.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.User.Handlers;

using Models;

public class ChangeUserRoleHandler : IRequestHandler<ChangeUserRoleCommand, SmallUserResult>
{
    private readonly IRepository<User,int> _userRepository;
    private readonly IRepository<Models.Entities.Role,int> _roleRepository;
    private readonly IMapper _mapper;

    public ChangeUserRoleHandler(IRepository<User, int> userRepository, IRepository<Models.Entities.Role, int> roleRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<SmallUserResult> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Table
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);

        if (user == null)
            throw new NotFoundException("User not found");
        
        var role = await _roleRepository.GetByIdAsync(request.RoleId);

        if (role == null)
            throw new NotFoundException("Role not found");

        user.Roles = null;
        user.Roles = new List<Models.Entities.Role>
        {
            role
        };

        user = await _userRepository.UpdateAsync(user);

        var userResult = _mapper.Map<SmallUserResult>(user);
        return userResult;
    }
}