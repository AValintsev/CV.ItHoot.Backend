using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Team.Commands;
using CVBuilder.Application.Team.Queries;
using CVBuilder.Application.Team.Responses;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Team.Handlers;
using Models.Entities;

public class ApproveTeamHandler : IRequestHandler<ApproveTeamCommand, TeamResult>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<Team, int> _teamRepository;

    public ApproveTeamHandler(IMapper mapper, IRepository<Team, int> teamRepository, IMediator mediator)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
        _mediator = mediator;
    }

    public async Task<TeamResult> Handle(ApproveTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.Table
            .Include(x => x.Resumes)
            .FirstOrDefaultAsync(x=>x.Id == request.TeamId, cancellationToken: cancellationToken);
        
        if (team == null)
        {
            throw new NotFoundException("Team not found");
        }
        
        foreach (var resume in team.Resumes)
        {
            var resumeRequest = request.Resumes.FirstOrDefault(x => x.Id == resume.Id);
            if (resumeRequest == null)
            {
                resume.StatusResume = StatusTeamResume.NotSelected;
            }
            else
            {
                resume.StatusResume = resumeRequest.IsSelected ? StatusTeamResume.Selected : StatusTeamResume.Denied;
            }
        }

        team.StatusTeam = team.Resumes.Any(x => x.StatusResume == StatusTeamResume.Selected)
            ? StatusTeam.Approved
            : StatusTeam.Denied;

        team = await _teamRepository.UpdateAsync(team);
        return await _mediator.Send(new GetTeamByIdQuery {Id = team.Id}, cancellationToken);
    }
}