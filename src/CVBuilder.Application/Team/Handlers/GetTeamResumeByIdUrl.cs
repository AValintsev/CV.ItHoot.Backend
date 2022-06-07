using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Team.Queries;
using CVBuilder.Application.Team.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Team.Handlers;
using Models.Entities;

public class GetTeamResumeByIdUrl:IRequestHandler<GetTeamResumeByUrlQuery, TeamResumeResult>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<TeamResume, int> _teamResumeRepository;

    public GetTeamResumeByIdUrl(IMapper mapper, IRepository<TeamResume, int> teamResumeRepository, IMediator mediator)
    {
        _mapper = mapper;
        _teamResumeRepository = teamResumeRepository;
        _mediator = mediator;
    }

    public async Task<TeamResumeResult> Handle(GetTeamResumeByUrlQuery request, CancellationToken cancellationToken)
    {
        var resume = await _teamResumeRepository.Table
            .Include(x=>x.ShortUrl)
            .FirstOrDefaultAsync(x => x.ShortUrl.Url == request.ShortUrl, cancellationToken: cancellationToken);

        if (resume == null)
        {
            throw new NotFoundException("Resume not found");
        }

        var command = new GetTeamResumeQuery()
        {
            UserRoles = request.UserRoles,
            UserId = request.UserId,
            TeamId = resume.TeamId,
            TeamResumeId = resume.Id
        };

        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
   
}