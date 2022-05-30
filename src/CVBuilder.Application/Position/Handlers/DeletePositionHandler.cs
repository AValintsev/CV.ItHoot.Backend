using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Position.Commands;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Position.Handlers;

using Models.Entities;

public class DeletePositionHandler : IRequestHandler<DeletePositionCommand, bool>
{
    private readonly IRepository<Position, int> _positionRepository;

    public DeletePositionHandler(IRepository<Position, int> positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<bool> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        await _positionRepository.DeleteAsync(request.PositionId);
        return true;
    }
}