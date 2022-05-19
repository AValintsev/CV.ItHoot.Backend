using System.Collections.Generic;
using CVBuilder.Application.Team.Responses;
using MediatR;

namespace CVBuilder.Application.Team.Commands;

public class CreateTeamCommand: IRequest<TeamResult>
{
    public int UserId { get; set; }
    public string TeamName { get; set; }
    public List<CreateResumeCommand> Resumes { get; set; }
}