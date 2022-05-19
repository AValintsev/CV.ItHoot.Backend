using System.Collections.Generic;
using CVBuilder.Application.Team.Responses;
using MediatR;

namespace CVBuilder.Application.Team.Commands;

public class UpdateTeamCommand:IRequest<TeamResult>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string TeamName { get; set; }
    public List<UpdateResumeCommand> Resumes { get; set; }
}