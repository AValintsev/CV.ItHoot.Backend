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
public class GetTeamByIdHandler: IRequestHandler<GetTeamByIdQuery, TeamResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Team, int> _teamRepository;

    public GetTeamByIdHandler(IMapper mapper, IRepository<Team, int> teamRepository)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
    }

    public async Task<TeamResult> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.Table
            .Include(x => x.Resumes)
            .ThenInclude(x => x.Resume)
            .Include(x=>x.Client)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        var result = _mapper.Map<TeamResult>(team);
        return result;
    }
   
}