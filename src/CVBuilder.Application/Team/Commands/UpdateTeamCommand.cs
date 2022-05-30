using System.Collections.Generic;
using CVBuilder.Application.Team.Responses;
using CVBuilder.Models;
using MediatR;

namespace CVBuilder.Application.Team.Commands;

public class UpdateTeamCommand:IRequest<TeamResult>
{
    public int Id { get; set; }
    public int? ClientId { get; set; }
    public StatusTeam StatusTeam { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowContacts { get; set; }
    public bool ResumeTemplateId { get; set; }
    public int UserId { get; set; }
    public string TeamName { get; set; }
    public List<UpdateResumeCommand> Resumes { get; set; }
}