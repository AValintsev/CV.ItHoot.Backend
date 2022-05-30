using CVBuilder.Application.Team.Responses;
using MediatR;

namespace CVBuilder.Application.Team.Queries;

public class GetTeamByIdQuery:IRequest<TeamResult>
{
    public int Id { get; set; }
}