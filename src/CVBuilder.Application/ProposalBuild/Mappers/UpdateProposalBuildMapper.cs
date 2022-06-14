using CVBuilder.Application.ProposalBuild.Commands;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.ProposalBuild.Mappers;
using Models.Entities;

public class UpdateProposalBuildMapper:AppMapperBase
{
    public UpdateProposalBuildMapper()
    {
        CreateMap<UpdateProposalBuildCommand, ProposalBuild>()
            .ForMember(x=>x.Id,y=>y.MapFrom(x=>x.Id));
        CreateMap<UpdateProposalBuildPositionCommand, ProposalBuildPosition>();
    }
}