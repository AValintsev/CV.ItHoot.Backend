using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalResumeQuery : IRequest<ProposalResumeResult>
{
    public List<string> UserRoles { get; set; }
    public int? UserId { get; set; }
    public int ProposalId { get; set; }
    public int ProposalResumeId { get; set; }
}