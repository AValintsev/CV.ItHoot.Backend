using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Team.Commands;
using CVBuilder.Application.Team.Queries;
using CVBuilder.Application.Team.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Team.Handlers;

using Models.Entities;

public class TeamCreateHandler : IRequestHandler<CreateTeamCommand, TeamResult>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<Team, int> _teamRepository;

    public TeamCreateHandler(IMapper mapper, IRepository<Team, int> teamRepository, IMediator mediator)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
        _mediator = mediator;
    }

    public async Task<TeamResult> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<Team>(request);
        model.ResumeTemplateId = model.ResumeTemplateId == 0 ? 1 : model.ResumeTemplateId;
        model = await _teamRepository.CreateAsync(model);
        var result = await _mediator.Send(new GetTeamByIdQuery {Id = model.Id}, cancellationToken);
        return result;
    }
   
}