using System.Collections.Generic;
using CVBuilder.Application.Team.Responses;
using MediatR;

namespace CVBuilder.Application.Team.Queries;

public class GetTeamResumeByUrlQuery:IRequest<TeamResumeResult>
{
    public string ShortUrl { get; set; }
    public List<string> UserRoles { get; set; }
    public int? UserId { get; set; }
}