using System.Collections.Generic;
using CVBuilder.Application.Team.Responses;
using MediatR;

namespace CVBuilder.Application.Team.Queries;

public class GetAllTeamsQuery:IRequest<List<SmallTeamResult>>
{

}