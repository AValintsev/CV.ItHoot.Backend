using CVBuilder.Application.TeamBuild.Result;
using MediatR;

namespace CVBuilder.Application.TeamBuild.Queries;

public class GetTeamBuildByIdQuery:IRequest<TeamBuildResult>
{
    public int TeamBuildId { get; set; }
}