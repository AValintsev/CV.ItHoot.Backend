using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Team.Commands;
using CVBuilder.Application.Team.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Team.Handlers;

using Models.Entities;

public class TeamCreateHandler : IRequestHandler<CreateTeamCommand, TeamResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Team, int> _teamRepository;

    public TeamCreateHandler(IMapper mapper, IRepository<Team, int> teamRepository)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
    }

    public async Task<TeamResult> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<Team>(request);
        model = await _teamRepository.CreateAsync(model);
        var result = _mapper.Map<TeamResult>(model);
        return result;
    }
   
}