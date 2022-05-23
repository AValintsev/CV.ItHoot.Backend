using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Team.Queries;
using CVBuilder.Application.Team.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Team.Handlers;
using Models.Entities;
public class GetAllTeamsHandler : IRequestHandler<GetAllTeamsQuery, List<SmallTeamResult>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Team, int> _teamRepository;

    public GetAllTeamsHandler(IMapper mapper, IRepository<Team, int> teamRepository)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
    }

    public async Task<List<SmallTeamResult>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        var teams = await _teamRepository.Table
            .Include(x => x.Resumes)
            .Include(x=>x.CreatedUser)
            .Include(x=>x.Client)
            .ToListAsync(cancellationToken: cancellationToken);
        var smallTeams = _mapper.Map<List<SmallTeamResult>>(teams);
        return smallTeams;
    }
   
}