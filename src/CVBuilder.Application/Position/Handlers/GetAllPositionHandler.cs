using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Position.Commands;
using CVBuilder.Application.Position.Queries;
using CVBuilder.Application.Position.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Position.Handlers;

using Models.Entities;

public class GetAllPositionHandler : IRequestHandler<GetAllPositionQuery, List<PositionResult>>
{
    private readonly IRepository<Position, int> _positionRepository;
    private readonly IMapper _mapper;

    public GetAllPositionHandler(IRepository<Position, int> positionRepository, IMapper mapper)
    {
        _positionRepository = positionRepository;
        _mapper = mapper;
    }

    public async Task<List<PositionResult>> Handle(GetAllPositionQuery request, CancellationToken cancellationToken)
    {
        var result = await _positionRepository.GetListAsync();
        return _mapper.Map<List<PositionResult>>(result);
    }
}