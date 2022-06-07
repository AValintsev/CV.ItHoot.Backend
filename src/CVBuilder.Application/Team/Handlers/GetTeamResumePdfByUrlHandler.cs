using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Team.Queries;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace CVBuilder.Application.Team.Handlers;

using Models.Entities;

public class GetTeamResumePdfByUrlHandler : IRequestHandler<GetTeamResumePdfByUrlQuery, Stream>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<TeamResume, int> _teamResumeRepository;


    public GetTeamResumePdfByUrlHandler(IMapper mapper, IRepository<TeamResume, int> teamResumeRepository, IMediator mediator)
    {
        _mapper = mapper;
        _teamResumeRepository = teamResumeRepository;
        _mediator = mediator;
    }

    public async Task<Stream> Handle(GetTeamResumePdfByUrlQuery request, CancellationToken cancellationToken)
    {
        var resume = await _teamResumeRepository.Table
            .Include(x=>x.ShortUrl)
            .FirstOrDefaultAsync(x => x.ShortUrl.Url == request.ShortUrl, cancellationToken: cancellationToken);

        if (resume == null)
        {
            throw new NotFoundException("Resume not found");
        }

        var command = new GetTeamResumePdfQuery()
        {
            TeamId = resume.TeamId,
            TeamResumeId = resume.Id,
            JwtToken = request.JwtToken
        };

        var result = await _mediator.Send(command, cancellationToken);
        return result;        
    }
}