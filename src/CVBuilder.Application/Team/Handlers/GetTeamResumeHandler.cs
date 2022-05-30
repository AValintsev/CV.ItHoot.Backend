using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Team.Queries;
using CVBuilder.Application.Team.Responses;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ResumeResult = CVBuilder.Application.Resume.Responses.CvResponse.ResumeResult;

namespace CVBuilder.Application.Team.Handlers;

using Models.Entities;

public class GetTeamResumeHandler:IRequestHandler<GetTeamResumeQuery, TeamResumeResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Team, int> _teamRepository;
    private readonly IMediator _mediator;

    public GetTeamResumeHandler(IMapper mapper, IRepository<Team, int> teamRepository, IMediator mediator)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
        _mediator = mediator;
    }

    public async Task<TeamResumeResult> Handle(GetTeamResumeQuery request, CancellationToken cancellationToken)
    {
        var team =
            await _teamRepository.Table
                .Include(x=>x.Resumes)
                .FirstOrDefaultAsync(x => x.Id == request.TeamId,
                cancellationToken: cancellationToken);
        
        if (team == null)
        {
            throw new NotFoundException("Team not found");
        }
        
        var resumeId = team.Resumes.FirstOrDefault(x => x.Id == request.TeamResumeId)?.ResumeId;
        
        if (resumeId == null)
        {
            throw new NotFoundException("Resume not found");
        }
        
        var resume = await _mediator.Send(new GetResumeByIdQuery()
        {
            Id = resumeId.GetValueOrDefault(),
            UserRoles = request.UserRoles,
            UserId = request.UserId
        }, cancellationToken);
        
        if (request.UserRoles.Contains(Enums.RoleTypes.Client.ToString()) && !team.ShowContacts)
        {
            HideContacts(resume);
        }
        
       
        return new TeamResumeResult
        {
            ShowLogo = team.ShowLogo,
            ResumeTemplateId = team.ResumeTemplateId,
            Resume = resume
        };
    }

    private void HideContacts(ResumeResult resume)
    {
        resume.Country = "ItHootland";
        resume.City = "ITHootland";
        resume.Street = "ItHootland";
        resume.Code = "0";
        resume.Email = "ithoot@gmail.com";
        resume.Site = "ithoot.com";
        resume.Phone = "380000000";
    }
}