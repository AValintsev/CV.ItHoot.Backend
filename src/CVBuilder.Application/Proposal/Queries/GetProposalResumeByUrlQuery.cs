using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalResumeByUrlQuery : IRequest<ProposalResumeResult>
{
    public string ShortUrl { get; set; }
    public List<string> UserRoles { get; set; }
    public int? UserId { get; set; }
}