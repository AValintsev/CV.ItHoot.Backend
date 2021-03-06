using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Complexity.Commands;
using CVBuilder.Application.Complexity.Result;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Complexity.Handlers;
using Models.Entities;

public class UpdateComplexityHandler: IRequestHandler<UpdateComplexityCommand, ComplexityResult>
{
    private readonly IRepository<TeamBuildComplexity, int> _complexityRepository;
    private readonly IMapper _mapper;

    public UpdateComplexityHandler(IRepository<TeamBuildComplexity, int> complexityRepository, IMapper mapper)
    {
        _complexityRepository = complexityRepository;
        _mapper = mapper;
    }

    public async Task<ComplexityResult> Handle(UpdateComplexityCommand request, CancellationToken cancellationToken)
    {
        var complexity = _mapper.Map<TeamBuildComplexity>(request);
        complexity = await _complexityRepository.UpdateAsync(complexity);
        var result = _mapper.Map<ComplexityResult>(complexity);
        return result;
    }
}