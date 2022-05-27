using System.Collections.Generic;
using CVBuilder.Application.Team.Responses;
using MediatR;

namespace CVBuilder.Application.Team.Queries;

public class GetAllTeamsQuery:IRequest<List<SmallTeamResult>>
{
    public int UserId { get; set; }
    public List<string> UserRoles { get; set; }
}