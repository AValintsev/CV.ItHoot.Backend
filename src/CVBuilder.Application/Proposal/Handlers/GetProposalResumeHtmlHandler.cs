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

        var resume = await _mediator.Send(new GetResumeByIdQuery()
        {
            Id = resumeId.GetValueOrDefault(),
            UserRoles = request.UserRoles,
            UserId = request.UserId
        }, cancellationToken);

        if (request.UserRoles.IsNullOrEmpty() ||
            (request.UserRoles.Contains(Enums.RoleTypes.Client.ToString()) && !proposal.ShowContacts))
        {
            HideContacts(resume);
        }

        if (request.UserRoles.IsNullOrEmpty() ||
            (request.UserRoles.Contains(Enums.RoleTypes.Client.ToString()) && !proposal.ShowCompanyNames))
        {
            HideCompanyNames(resume);
        }

        var template = await _templateRepository.GetByIdAsync(proposal.ResumeTemplateId);

        if (template == null)
        {
            // throw new NullReferenceException("Template not foumnd");
            template = await _templateRepository.GetByIdAsync(4);
        }

        var builder = new ResumeTemplateBuilder(template.Html);
        var html = await builder.BindTemplateAsync(resume);


        return html;
    }

    private static void HideCompanyNames(ResumeResult resume)
    {
        foreach (var experience in resume.Experiences)
        {
            experience.Company = "Company";
        }
    }

    private static void HideContacts(ResumeResult resume)
    {
        resume.Country = "ItHootland";
        resume.City = "ITHootland";
        resume.Street = "ItHootland";
        resume.Code = "0";
        resume.Email = "ithoot@gmail.com";
        resume.Site = "ithoot.com";
        resume.Phone = "380000000";
    }
}