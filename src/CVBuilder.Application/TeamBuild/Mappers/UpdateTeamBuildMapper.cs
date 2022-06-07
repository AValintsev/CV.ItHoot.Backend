using CVBuilder.Application.TeamBuild.Commands;

namespace CVBuilder.Application.TeamBuild.Mappers;
using Models.Entities;

public class UpdateTeamBuildMapper:AppMapperBase
{
    public UpdateTeamBuildMapper()
    {
        CreateMap<UpdateTeamBuildCommand, TeamBuild>()
            .ForMember(x=>x.Id,y=>y.MapFrom(x=>x.Id));
        CreateMap<UpdateTeamBuildPositionCommand, TeamBuildPosition>();
    }
}