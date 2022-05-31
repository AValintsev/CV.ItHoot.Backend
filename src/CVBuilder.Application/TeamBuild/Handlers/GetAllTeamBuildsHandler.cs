using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.TeamBuild.Queries;
using CVBuilder.Application.TeamBuild.Result;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.TeamBuild.Handlers;
using Models.Entities;

public class GetAllTeamBuildsHandler : IRequestHandler<GetAllTeamBuildsQuery, List<TeamBuildResult>>
{
    private readonly IRepository<TeamBuild, int> _teamBuildRepository;
    private readonly IMapper _mapper;

    public GetAllTeamBuildsHandler(IRepository<TeamBuild, int> teamBuildRepository, IMapper mapper)
    {
        _teamBuildRepository = teamBuildRepository;
        _mapper = mapper;
    }

    public async Task<List<TeamBuildResult>> Handle(GetAllTeamBuildsQuery request, CancellationToken cancellationToken)
    {
        var teamBuild = await _teamBuildRepository.Table
            .Include(x => x.Complexity)
            .Include(x => x.Positions)
            .ThenInclude(x => x.Position)
            .ToListAsync(cancellationToken: cancellationToken);

        if (teamBuild == null)
        {
            throw new NotFoundException("Team build not found");
        }

        var result = _mapper.Map<List<TeamBuildResult>>(teamBuild);
        return result;
    }
}