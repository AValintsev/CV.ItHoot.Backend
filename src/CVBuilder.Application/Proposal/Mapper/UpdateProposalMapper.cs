using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Models;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Proposal.Mapper;
using Models.Entities;

public class UpdateProposalMapper: AppMapperBase
{
    public UpdateProposalMapper()
    {
        CreateMap<UpdateProposalCommand, Proposal>();
        CreateMap<UpdateResumeCommand, ProposalResume>()
            .ForMember(x => x.StatusResume, y => y.MapFrom(z => StatusProposalResume.NotSelected));
    }
}