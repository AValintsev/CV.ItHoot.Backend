using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

public class GetAllArchiveProposalsHandler : IRequestHandler<GetAllArchiveProposalsQuery, List<SmallProposalResult>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.Proposal, int> _proposalRepository;

    public GetAllArchiveProposalsHandler(IMapper mapper, IRepository<Models.Entities.Proposal, int> proposalRepository)
    {
        _mapper = mapper;
        _proposalRepository = proposalRepository;
    }

    public async Task<List<SmallProposalResult>> Handle(GetAllArchiveProposalsQuery request,
        CancellationToken cancellationToken)
    {
        var proposals = await _proposalRepository.Table
            .Where(x => x.StatusProposal == StatusProposal.Done)
            .Include(x => x.Resumes)
            .Include(x => x.CreatedUser)
            .Include(x => x.Client)
            .ToListAsync(cancellationToken: cancellationToken);
        var smallProposals = _mapper.Map<List<SmallProposalResult>>(proposals);
        return smallProposals;
    }
}