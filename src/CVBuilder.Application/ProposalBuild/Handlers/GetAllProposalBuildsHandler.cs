using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.ProposalBuild.Queries;
using CVBuilder.Application.ProposalBuild.Result;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.ProposalBuild.Handlers;

using Models.Entities;

public class GetAllProposalBuildsHandler : IRequestHandler<GetAllProposalBuildsQuery, List<ProposalBuildResult>>
{
    private readonly IRepository<ProposalBuild, int> _proposalBuildRepository;
    private readonly IMapper _mapper;

    public GetAllProposalBuildsHandler(IRepository<ProposalBuild, int> proposalBuildRepository, IMapper mapper)
    {
        _proposalBuildRepository = proposalBuildRepository;
        _mapper = mapper;
    }

    public async Task<List<ProposalBuildResult>> Handle(GetAllProposalBuildsQuery request,
        CancellationToken cancellationToken)
    {
        var proposalBuilds = await _proposalBuildRepository.Table
            .Include(x => x.Complexity)
            .Include(x => x.Positions)
            .ThenInclude(x => x.Position)
            .ToListAsync(cancellationToken: cancellationToken);

        if (proposalBuilds == null)
        {
            throw new NotFoundException("Proposal build not found");
        }

        var result = _mapper.Map<List<ProposalBuildResult>>(proposalBuilds);
        return result;
    }
}