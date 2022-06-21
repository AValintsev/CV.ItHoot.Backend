using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetAllProposalsQuery : IRequest<List<SmallProposalResult>>
{
    public int UserId { get; set; }
    public List<string> UserRoles { get; set; }
}