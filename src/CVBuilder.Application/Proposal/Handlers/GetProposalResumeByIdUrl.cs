using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

public class GetProposalResumeByIdUrl : IRequestHandler<GetProposalResumeByUrlQuery, string>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<ProposalResume, int> _proposalResumeRepository;

    public GetProposalResumeByIdUrl(IMapper mapper, IRepository<ProposalResume, int> proposalResumeRepository,
        IMediator mediator)
    {
        _mapper = mapper;
        _proposalResumeRepository = proposalResumeRepository;
        _mediator = mediator;
    }

    public async Task<string> Handle(GetProposalResumeByUrlQuery request,
        CancellationToken cancellationToken)
    {
        var resume = await _proposalResumeRepository.Table
            .Include(x => x.ShortUrl)
            .FirstOrDefaultAsync(x => x.ShortUrl.Url == request.ShortUrl, cancellationToken: cancellationToken);

        if (resume == null)
        {
            throw new NotFoundException("Resume not found");
        }

        var command = new GetProposalResumeHtmlQuery()
        {
            UserRoles = request.UserRoles,
            UserId = request.UserId,
            ProposalId = resume.ProposalId,
            ProposalResumeId = resume.Id
        };

        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}