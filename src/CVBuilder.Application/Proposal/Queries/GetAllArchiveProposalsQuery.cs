using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetAllArchiveProposalsQuery:IRequest<List<SmallProposalResult>>
{
    
}