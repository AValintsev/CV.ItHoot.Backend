using System.Collections.Generic;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalResumeHtmlQuery : IRequest<string>
{
    public List<string> UserRoles { get; set; }
    public int? UserId { get; set; }
    public int ProposalId { get; set; }
    public int ProposalResumeId { get; set; }
}