using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

public class GetResumesByProposalBuildHandler : IRequestHandler<GetResumesByProposalBuildQuery, List<ResumeCardResult>>
{
    private readonly IDeletableRepository<Resume, int> _resumeRepository;
    private readonly IRepository<ProposalBuild, int> _proposalBuildRepository;
    private readonly IMapper _mapper;

    public GetResumesByProposalBuildHandler(IDeletableRepository<Resume, int> resumeRepository, IMapper mapper, IRepository<ProposalBuild, int> proposalBuildRepository)
    {
        _resumeRepository = resumeRepository;
        _mapper = mapper;
        _proposalBuildRepository = proposalBuildRepository;
    }

    public async Task<List<ResumeCardResult>> Handle(GetResumesByProposalBuildQuery request,
        CancellationToken cancellationToken)
    {
        var proposalBuild = await _proposalBuildRepository.Table
            .Include(x => x.Positions)
            .FirstOrDefaultAsync(x => x.Id == request.ProposalBuildId, cancellationToken: cancellationToken);

        if (proposalBuild == null)
        {
            throw new NotFoundException("Proposal Build not found");
        }

        var positions = proposalBuild.Positions.Select(x => x.PositionId).ToList();

        var templates = await _resumeRepository.Table
            .Include(x => x.LevelSkills)
            .ThenInclude(x => x.Skill)
            .Include(x => x.Position)
            .Where(x => positions.Contains(x.PositionId.GetValueOrDefault()))
            .ToListAsync(cancellationToken: cancellationToken);
        var result = _mapper.Map<List<ResumeCardResult>>(templates);
        return result;
    }
}