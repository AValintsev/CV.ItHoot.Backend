using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.ProposalBuild.Commands;
using CVBuilder.Application.ProposalBuild.Queries;
using CVBuilder.Application.ProposalBuild.Result;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.ProposalBuild.Handlers;
using Models.Entities;

public class CreateProposalBuildHandler:IRequestHandler<CreateProposalBuildCommand, ProposalBuildResult>
{
    private readonly IRepository<ProposalBuild, int> _proposalBuildRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    
    public CreateProposalBuildHandler(IRepository<ProposalBuild, int> proposalBuildRepository, IMapper mapper, IMediator mediator)
    {
        _proposalBuildRepository = proposalBuildRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ProposalBuildResult> Handle(CreateProposalBuildCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<ProposalBuild>(request);
        model = await _proposalBuildRepository.CreateAsync(model);

        var command = new GetProposalBuildByIdQuery()
        {
            ProposalBuildId = model.Id
        };
        
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}