using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

using Models.Entities;

public class GetProposalByIdHandler : IRequestHandler<GetProposalByIdQuery, ProposalResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Proposal, int> _proposalRepository;

    public GetProposalByIdHandler(IMapper mapper, IRepository<Proposal, int> proposalRepository)
    {
        _mapper = mapper;
        _proposalRepository = proposalRepository;
    }

    public async Task<ProposalResult> Handle(GetProposalByIdQuery request, CancellationToken cancellationToken)
    {
        var proposal = await _proposalRepository.Table
            .Include(x => x.Client)
            .ThenInclude(x => x.ShortUrl)
            .Include(x => x.ProposalBuild)
            .Include(x => x.Resumes)
            .ThenInclude(x => x.Resume)
            .ThenInclude(x => x.LevelSkills)
            .ThenInclude(x => x.Skill)
            .Include(x => x.Resumes)
            .ThenInclude(x => x.Resume)
            .ThenInclude(x => x.Image)
            .Include(x => x.Resumes)
            .ThenInclude(x => x.Resume)
            .ThenInclude(x => x.Position)
            .Include(x => x.Client)
            .Include(x => x.Resumes)
            .ThenInclude(x => x.ShortUrl)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (proposal == null)
        {
            throw new NotFoundException("Proposal not found");
        }

        var result = _mapper.Map<ProposalResult>(proposal);
        return result;
    }
}