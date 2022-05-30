using System.Collections.Generic;
using CVBuilder.Application.Team.Responses;
using MediatR;

namespace CVBuilder.Application.Team.Commands;

public class ApproveTeamCommand:IRequest<TeamResult>
{
    public int TeamId { get; set; }
    public List<ApproveTeamResumeCommand>Resumes { get; set; }
}