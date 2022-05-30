using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Position.Commands;
using CVBuilder.Application.Position.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Position.Handlers;

using Models.Entities;

public class UpdatePositionHandler: IRequestHandler<UpdatePositionCommand, PositionResult>
{
    private readonly IRepository<Position, int> _positionRepository;
    private readonly IMapper _mapper;

    public UpdatePositionHandler(IRepository<Position, int> positionRepository, IMapper mapper)
    {
        _positionRepository = positionRepository;
        _mapper = mapper;
    }

    public async Task<PositionResult> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        var model = new Position()
        {
            Id = request.PositionId,
            PositionName = request.PositionName,
            UpdatedAt = DateTime.Now
        };

        var result = await _positionRepository.UpdateAsync(model);
        return _mapper.Map<PositionResult>(result);
    }
}