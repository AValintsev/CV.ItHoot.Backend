using CVBuilder.Application.ProposalBuild.Result;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.ProposalBuild.Mappers;

using Models.Entities;

public class ProposalBuildResultMapper : AppMapperBase
{
    public ProposalBuildResultMapper()
    {
        CreateMap<ProposalBuild, ProposalBuildResult>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));

        CreateMap<ProposalBuildComplexity, ComplexityResult>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));

        CreateMap<ProposalBuildPosition, ProposalBuildPositionResult>()
            .ForMember(x => x.PositionName, y => y.MapFrom(x => x.Position.PositionName));
    }
}