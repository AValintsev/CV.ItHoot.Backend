using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Team.Commands;
using CVBuilder.Application.Team.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Team.Handlers;

using Models.Entities;

public class UpdateTeamHandler : IRequestHandler<UpdateTeamCommand, TeamResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Team, int> _teamRepository;

    public UpdateTeamHandler(IMapper mapper, IRepository<Team, int> teamRepository)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
    }

    public async Task<TeamResult> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = _mapper.Map<Team>(request);
        var teamDto = await _teamRepository.Table
            .Include(x => x.Resumes)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        UpdateTeam(teamDto, team);
        teamDto = await _teamRepository.UpdateAsync(teamDto);
        var result = _mapper.Map<TeamResult>(teamDto);
        return result;
    }


    private void UpdateTeam(Team teamDto, Team team)
    {
        teamDto.UpdatedAt = DateTime.UtcNow;
        teamDto.TeamName = team.TeamName;
        teamDto.Resumes = team.Resumes;
    }
}