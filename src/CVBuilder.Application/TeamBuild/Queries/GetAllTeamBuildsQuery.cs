using System.Collections.Generic;
using CVBuilder.Application.TeamBuild.Result;
using MediatR;

namespace CVBuilder.Application.TeamBuild.Queries;

public class GetAllTeamBuildsQuery : IRequest<List<TeamBuildResult>>
{
}