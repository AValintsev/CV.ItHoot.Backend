using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.TeamBuild.Commands;
using CVBuilder.Application.TeamBuild.Queries;
using CVBuilder.Application.TeamBuild.Result;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.TeamBuild.Handlers;
using Models.Entities;

public class CreateTeamBuildHandler:IRequestHandler<CreateTeamBuildCommand, TeamBuildResult>
{
    private readonly IRepository<TeamBuild, int> _teamBuildRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    
    public CreateTeamBuildHandler(IRepository<TeamBuild, int> teamBuildRepository, IMapper mapper, IMediator mediator)
    {
        _teamBuildRepository = teamBuildRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<TeamBuildResult> Handle(CreateTeamBuildCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<TeamBuild>(request);
        model = await _teamBuildRepository.CreateAsync(model);

        var command = new GetTeamBuildByIdQuery()
        {
            TeamBuildId = model.Id
        };
        
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}