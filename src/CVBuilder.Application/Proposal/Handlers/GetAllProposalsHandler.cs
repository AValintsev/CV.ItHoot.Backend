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

using Models.Entities;

public class GetAllProposalsHandler : IRequestHandler<GetAllProposalsQuery, List<SmallProposalResult>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Proposal, int> _proposalRepository;

    public GetAllProposalsHandler(IMapper mapper, IRepository<Proposal, int> proposalRepository)
    {
        _mapper = mapper;
        _proposalRepository = proposalRepository;
    }

    public async Task<List<SmallProposalResult>> Handle(GetAllProposalsQuery request,
        CancellationToken cancellationToken)
    {
        List<Proposal> proposals;
        if (request.UserRoles.Contains(Enums.RoleTypes.Admin.ToString()))
        {
            proposals = await _proposalRepository.Table
                .Where(x => x.StatusProposal != StatusProposal.Done)
                .Include(x => x.Resumes)
                .Include(x => x.CreatedUser)
                .Include(x => x.Client)
                .ToListAsync(cancellationToken: cancellationToken);
        }
        else if (request.UserRoles.Contains(Enums.RoleTypes.Client.ToString()))
        {
            proposals = await _proposalRepository.Table
                .Where(x => x.ClientId == request.UserId)
                .Include(x => x.Resumes)
                .Include(x => x.CreatedUser)
                .Include(x => x.Client)
                .ToListAsync(cancellationToken: cancellationToken);
        }
        else
        {
            proposals = new List<Proposal>();
        }

        var smallProposals = _mapper.Map<List<SmallProposalResult>>(proposals);
        return smallProposals;
    }
}