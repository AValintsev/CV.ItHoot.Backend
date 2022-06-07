using CVBuilder.Application.TeamBuild.Result;

namespace CVBuilder.Application.TeamBuild.Mappers;

using Models.Entities;

public class TeamBuildResultMapper : AppMapperBase
{
    public TeamBuildResultMapper()
    {
        CreateMap<TeamBuild, TeamBuildResult>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));

        CreateMap<TeamBuildComplexity, ComplexityResult>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));

        CreateMap<TeamBuildPosition, TeamBuildPositionResult>()
            .ForMember(x => x.PositionName, y => y.MapFrom(x => x.Position.PositionName));
    }
}