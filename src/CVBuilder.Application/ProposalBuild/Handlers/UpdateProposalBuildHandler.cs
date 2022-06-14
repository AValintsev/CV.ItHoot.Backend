using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.ProposalBuild.Commands;
using CVBuilder.Application.ProposalBuild.Queries;
using CVBuilder.Application.ProposalBuild.Result;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.ProposalBuild.Handlers;
using Models.Entities;

public class UpdateProposalBuildHandler:IRequestHandler<UpdateProposalBuildCommand, ProposalBuildResult>
{
    private readonly IRepository<ProposalBuild, int> _proposalBuildRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    
    public UpdateProposalBuildHandler(IRepository<ProposalBuild, int> proposalBuildRepository, IMapper mapper, IMediator mediator)
    {
        _proposalBuildRepository = proposalBuildRepository;
        _mapper = mapper;
        _mediator = mediator;
    }
 
    public async Task<ProposalBuildResult> Handle(UpdateProposalBuildCommand request, CancellationToken cancellationToken)
    {
        var proposalBuild = _mapper.Map<ProposalBuild>(request);
        var proposalBuildDto = await _proposalBuildRepository.Table
            .Include(x=>x.Complexity)
            .Include(x=>x.Positions)
            .ThenInclude(x=>x.Position)
            .FirstOrDefaultAsync(x=>x.Id == proposalBuild.Id, cancellationToken: cancellationToken);
        
        if (proposalBuildDto == null)
        {
            throw new NotFoundException("Proposal Build not found");
        }

        UpdateProposalBuild(proposalBuildDto, proposalBuild);
        proposalBuildDto = await _proposalBuildRepository.UpdateAsync(proposalBuildDto);
        var result = await _mediator
            .Send(new GetProposalBuildByIdQuery(){ProposalBuildId = proposalBuildDto.Id}, cancellationToken);

        return result;
    }

    private void UpdateProposalBuild(ProposalBuild proposalBuildDto, ProposalBuild proposalBuild)
    {
        proposalBuildDto.UpdatedAt = DateTime.UtcNow;
        proposalBuildDto.Estimation = proposalBuild.Estimation;
        proposalBuildDto.ProjectTypeName = proposalBuild.ProjectTypeName;
        proposalBuildDto.ComplexityId = proposalBuild.ComplexityId;
        proposalBuildDto.Positions = proposalBuild.Positions;
    }
}