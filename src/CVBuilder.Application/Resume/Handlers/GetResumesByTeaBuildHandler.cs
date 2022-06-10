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

public class GetResumesByTeaBuildHandler : IRequestHandler<GetResumesByTeamBuildQuery, List<ResumeCardResult>>
{
    private readonly IDeletableRepository<Resume, int> _resumeRepository;
    private readonly IRepository<TeamBuild, int> _teamBuildRepository;
    private readonly IMapper _mapper;

    public GetResumesByTeaBuildHandler(IDeletableRepository<Resume, int> resumeRepository, IMapper mapper, IRepository<TeamBuild, int> teamBuildRepository)
    {
        _resumeRepository = resumeRepository;
        _mapper = mapper;
        _teamBuildRepository = teamBuildRepository;
    }

    public async Task<List<ResumeCardResult>> Handle(GetResumesByTeamBuildQuery request,
        CancellationToken cancellationToken)
    {
        var teamBuild = await _teamBuildRepository.Table
            .Include(x => x.Positions)
            .FirstOrDefaultAsync(x=>x.Id ==request.TeamBuildId, cancellationToken: cancellationToken);
        
        if (teamBuild == null)
        {
            throw new NotFoundException("Team Build not found");
        }

        var positions = teamBuild.Positions.Select(x=>x.PositionId).ToList();
        
        var templates = await _resumeRepository.Table
            .Include(x => x.LevelSkills)
            .ThenInclude(x => x.Skill)
            .Include(x => x.Position)
            .Where(x=> positions.Contains(x.PositionId.GetValueOrDefault()))
            .ToListAsync(cancellationToken: cancellationToken);
        var result = _mapper.Map<List<ResumeCardResult>>(templates);
        return result;
    }
}