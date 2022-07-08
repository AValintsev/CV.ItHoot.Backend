using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

public class GetProposalResumePdfByUrlHandler : IRequestHandler<GetProposalResumePdfByUrlQuery, Stream>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<ProposalResume, int> _proposalResumeRepository;


    public GetProposalResumePdfByUrlHandler(IMapper mapper, IRepository<ProposalResume, int> proposalResumeRepository,
        IMediator mediator)
    {
        _mapper = mapper;
        _proposalResumeRepository = proposalResumeRepository;
        _mediator = mediator;
    }

    public async Task<Stream> Handle(GetProposalResumePdfByUrlQuery request, CancellationToken cancellationToken)
    {
        var resume = await _proposalResumeRepository.Table
            .Include(x => x.ShortUrl)
            .FirstOrDefaultAsync(x => x.ShortUrl.Url == request.ShortUrl, cancellationToken: cancellationToken);

        if (resume == null)
        {
            throw new NotFoundException("Resume not found");
        }

        var command = new GetProposalResumePdfQuery()
        {
            ProposalId = resume.ProposalId,
            ProposalResumeId = resume.Id,
            UserId = request.UserId,
            UserRoles = request.UserRoles,
        };

        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}