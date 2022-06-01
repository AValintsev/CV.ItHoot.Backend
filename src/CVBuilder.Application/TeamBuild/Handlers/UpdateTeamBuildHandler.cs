using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.TeamBuild.Commands;
using CVBuilder.Application.TeamBuild.Queries;
using CVBuilder.Application.TeamBuild.Result;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.TeamBuild.Handlers;
using Models.Entities;

public class UpdateTeamBuildHandler:IRequestHandler<UpdateTeamBuildCommand, TeamBuildResult>
{
    private readonly IRepository<TeamBuild, int> _teamBuildRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    
    public UpdateTeamBuildHandler(IRepository<TeamBuild, int> teamBuildRepository, IMapper mapper, IMediator mediator)
    {
        _teamBuildRepository = teamBuildRepository;
        _mapper = mapper;
        _mediator = mediator;
    }
 
    public async Task<TeamBuildResult> Handle(UpdateTeamBuildCommand request, CancellationToken cancellationToken)
    {
        var teamBuild = _mapper.Map<TeamBuild>(request);
        var teamBuildDto = await _teamBuildRepository.Table
            .Include(x=>x.Complexity)
            .Include(x=>x.Positions)
            .ThenInclude(x=>x.Position)
            .FirstOrDefaultAsync(x=>x.Id == teamBuild.Id, cancellationToken: cancellationToken);
        
        if (teamBuildDto == null)
        {
            throw new NotFoundException("TeamBuild not found");
        }

        UpdateTeamBuild(teamBuildDto, teamBuild);
        teamBuildDto = await _teamBuildRepository.UpdateAsync(teamBuildDto);
        var result = await _mediator
            .Send(new GetTeamBuildByIdQuery(){TeamBuildId = teamBuildDto.Id}, cancellationToken);

        return result;
    }

    private void UpdateTeamBuild(TeamBuild teamBuildDto, TeamBuild teamBuild)
    {
        teamBuildDto.UpdatedAt = DateTime.UtcNow;
        teamBuildDto.EstimationName = teamBuild.EstimationName;
        teamBuildDto.ProjectTypeName = teamBuild.ProjectTypeName;
        teamBuildDto.ComplexityId = teamBuild.ComplexityId;
        teamBuildDto.Positions = teamBuild.Positions;
    }
}