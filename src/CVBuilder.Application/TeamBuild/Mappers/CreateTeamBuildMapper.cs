using System;
using CVBuilder.Application.TeamBuild.Commands;

namespace CVBuilder.Application.TeamBuild.Mappers;
using Models.Entities;

public class CreateTeamBuildMapper: AppMapperBase
{
    public CreateTeamBuildMapper()
    {
        CreateMap<CreateTeamBuildCommand, TeamBuild>()
            .ForMember(x=>x.CreatedAt,y=>y.MapFrom(z=>DateTime.UtcNow))
            .ForMember(x=>x.UpdatedAt,y=>y.MapFrom(z=>DateTime.UtcNow));
        CreateMap<CreateTeamBuildPositionCommand, TeamBuildPosition>();
    }
}