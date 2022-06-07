using CVBuilder.Application.TeamBuild.Commands;
using CVBuilder.Web.Contracts.V1.Requests.TeamBuild;

namespace CVBuilder.Web.Mappers;

public class TeamBuildMapper:MapperBase
{
    public TeamBuildMapper()
    {
        CreateMap<CreateTeamBuildRequest, CreateTeamBuildCommand>();
        CreateMap<CreateTeamBuildPositionRequest, CreateTeamBuildPositionCommand>();

        CreateMap<UpdateTeamBuildRequest, UpdateTeamBuildCommand>();
        CreateMap<UpdateTeamBuildPositionRequest, UpdateTeamBuildPositionCommand>();
    }
}