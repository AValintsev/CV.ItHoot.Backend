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

public class GetProposalBuildByIdHandler:IRequestHandler<GetProposalBuildByIdQuery, ProposalBuildResult>
{
    private readonly IRepository<ProposalBuild, int> _proposalBuildRepository;
    private readonly IMapper _mapper;
    public GetProposalBuildByIdHandler(IRepository<ProposalBuild, int> proposalBuildRepository, IMapper mapper)
    {
        _proposalBuildRepository = proposalBuildRepository;
        _mapper = mapper;
    }

    public async Task<ProposalBuildResult> Handle(GetProposalBuildByIdQuery request, CancellationToken cancellationToken)
    {
        var proposalBuild = await _proposalBuildRepository.Table
            .Include(x=>x.Complexity)
            .Include(x=>x.Positions)
            .ThenInclude(x=>x.Position)
            .FirstOrDefaultAsync(x => x.Id == request.ProposalBuildId, cancellationToken: cancellationToken);
       
        if (proposalBuild == null)
        {
            throw new NotFoundException("Proposal build not found");
        }
        
        var result = _mapper.Map<ProposalBuildResult>(proposalBuild);
        return result;
    }
}