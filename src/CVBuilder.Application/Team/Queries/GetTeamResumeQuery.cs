using System.Collections.Generic;
using CVBuilder.Application.Team.Responses;
using MediatR;

namespace CVBuilder.Application.Team.Queries;

public class GetTeamResumeQuery:IRequest<TeamResumeResult>
{
    public List<string> UserRoles { get; set; }
    public int TeamId { get; set; }
    public int TeamResumeId { get; set; }
}