using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Role.Queries;
using CVBuilder.Application.Role.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Role;

using Models.Entities;

public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<RoleResult>>
{
    private readonly IRepository<Role, int> _roleRepository;
    private readonly IMapper _mapper;

    public GetAllRolesHandler(IRepository<Role, int> roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<List<RoleResult>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.Table.ToListAsync(cancellationToken: cancellationToken);

        var roleResult = _mapper.Map<List<RoleResult>>(roles);
        return roleResult;
    }
}