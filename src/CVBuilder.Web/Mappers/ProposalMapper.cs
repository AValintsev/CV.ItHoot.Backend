using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Web.Contracts.V1.Requests.Proposal;

namespace CVBuilder.Web.Mappers;

public class ProposalMapper:MapperBase
{
    public ProposalMapper()
    {
        CreateMap<CreateProposalRequest, CreateProposalCommand>();
        CreateMap<CreateResumeRequest, CreateResumeCommand>();
        CreateMap<UpdateProposalRequest, UpdateProposalCommand>();
        CreateMap<UpdateResumeRequest, UpdateResumeCommand>();
        CreateMap<ApproveProposalRequest, ApproveProposalCommand>();
        CreateMap<ApproveProposalResumeRequest, ApproveProposalResumeCommand>();
        CreateMap<GetAllProposalsRequest, GetAllProposalsQuery>();
        CreateMap<GetAllProposalsRequest, GetAllArchiveProposalsQuery>();
    }

}