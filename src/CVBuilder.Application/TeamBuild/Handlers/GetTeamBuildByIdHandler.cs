using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.TeamBuild.Queries;
using CVBuilder.Application.TeamBuild.Result;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.TeamBuild.Handlers;
using Models.Entities;

public class GetTeamBuildByIdHandler:IRequestHandler<GetTeamBuildByIdQuery, TeamBuildResult>
{
    private readonly IRepository<TeamBuild, int> _teamBuildRepository;
    private readonly IMapper _mapper;
    public GetTeamBuildByIdHandler(IRepository<TeamBuild, int> teamBuildRepository, IMapper mapper)
    {
        _teamBuildRepository = teamBuildRepository;
        _mapper = mapper;
    }

    public async Task<TeamBuildResult> Handle(GetTeamBuildByIdQuery request, CancellationToken cancellationToken)
    {
        var teamBuild = await _teamBuildRepository.Table
            .Include(x=>x.Complexity)
            .Include(x=>x.Positions)
            .ThenInclude(x=>x.Position)
            .FirstOrDefaultAsync(x => x.Id == request.TeamBuildId, cancellationToken: cancellationToken);
       
        if (teamBuild == null)
        {
            throw new NotFoundException("Team build not found");
        }
        
        var result = _mapper.Map<TeamBuildResult>(teamBuild);
        return result;
    }
}