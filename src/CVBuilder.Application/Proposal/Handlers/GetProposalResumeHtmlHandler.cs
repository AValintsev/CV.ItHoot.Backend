using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Services;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ResumeResult = CVBuilder.Application.Resume.Responses.CvResponse.ResumeResult;

namespace CVBuilder.Application.Proposal.Handlers;

using Models.Entities;

public class GetProposalResumeHtmlHandler : IRequestHandler<GetProposalResumeHtmlQuery, string>
{
    private readonly IRepository<Proposal, int> _proposalRepository;
    private readonly IRepository<ResumeTemplate, int> _templateRepository;
    private readonly IMediator _mediator;

    public GetProposalResumeHtmlHandler(IRepository<Proposal, int> proposalRepository,
        IMediator mediator, IRepository<ResumeTemplate, int> templateRepository)
    {
        _proposalRepository = proposalRepository;
        _mediator = mediator;
        _templateRepository = templateRepository;
    }

    public async Task<string> Handle(GetProposalResumeHtmlQuery request, CancellationToken cancellationToken)
    {
        var proposal =
            await _proposalRepository.Table
                .Include(x => x.Resumes)
                .FirstOrDefaultAsync(x => x.Id == request.ProposalId,
                    cancellationToken: cancellationToken);

        if (proposal == null)
        {
            throw new NotFoundException("Proposal not found");
        }

        var resumeId = proposal.Resumes.FirstOrDefault(x => x.Id == request.ProposalResumeId)?.ResumeId;

        if (resumeId == null)
        {
            throw new NotFoundException("Resume not found");
        }

        var resume = await _mediator.Send(new GetProposalResumeQuery()
        {
            ProposalId = proposal.Id,
            ProposalResumeId = request.ProposalResumeId,
            UserRoles = request.UserRoles,
            UserId = request.UserId
        }, cancellationToken);


        var template = await _templateRepository.GetByIdAsync(proposal.ResumeTemplateId);

        if (template == null)
        {
            throw new NullReferenceException("Template not foumnd");
        }

        var builder = new ResumeTemplateBuilder(template.Html);
        var html = await builder.BindTemplateAsync(resume);


        return html;
    }
}