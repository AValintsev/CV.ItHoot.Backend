using System.Collections.Generic;
using CVBuilder.Application.Team.Responses;
using CVBuilder.Application.TeamBuild.Result;
using MediatR;

namespace CVBuilder.Application.Team.Commands;

public class CreateTeamCommand: IRequest<TeamResult>, IRequest<TeamBuildResult>
{
    public int UserId { get; set; }
    public int ClientId { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowContacts { get; set; }
    public string TeamName { get; set; }
    public int ResumeTemplateId { get; set; }
    public List<CreateResumeCommand> Resumes { get; set; }
}