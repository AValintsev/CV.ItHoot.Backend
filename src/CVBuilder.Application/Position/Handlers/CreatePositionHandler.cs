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

public class CreatePositionHandler : IRequestHandler<CreatePositionCommand, PositionResult>
{
    private readonly IRepository<Position, int> _positionRepository;
    private readonly IMapper _mapper;

    public CreatePositionHandler(IRepository<Position, int> positionRepository, IMapper mapper)
    {
        _positionRepository = positionRepository;
        _mapper = mapper;
    }

    public async Task<PositionResult> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.PositionName))
        {
            request.PositionName = "";
        }

        var model = new Position
        {
            PositionName = request.PositionName,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var result = await _positionRepository.CreateAsync(model);
        return _mapper.Map<PositionResult>(result);
    }
}