using System.Collections.Generic;
using CVBuilder.Application.Team.Responses;
using MediatR;

namespace CVBuilder.Application.Team.Commands;

public class CreateTeamCommand: IRequest<TeamResult>
{
    public int UserId { get; set; }
    public int? TeamBuildId { get; set; }
    public int ClientId { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowContacts { get; set; }
    public bool ShowCompanyNames { get; set; }
    public string TeamName { get; set; }
    public int ResumeTemplateId { get; set; }
    public List<CreateResumeCommand> Resumes { get; set; }
}