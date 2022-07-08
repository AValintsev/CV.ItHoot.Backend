using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ResumeResult = CVBuilder.Application.Resume.Responses.CvResponse.ResumeResult;

namespace CVBuilder.Application.Proposal.Handlers;

using Models.Entities;

public class GetProposalResumeHandler : IRequestHandler<GetProposalResumeQuery, ProposalResumeResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Proposal, int> _proposalRepository;
    private readonly IMediator _mediator;

    public GetProposalResumeHandler(IMapper mapper, IRepository<Proposal, int> proposalRepository, IMediator mediator)
    {
        _mapper = mapper;
        _proposalRepository = proposalRepository;
        _mediator = mediator;
    }

    public async Task<ProposalResumeResult> Handle(GetProposalResumeQuery request, CancellationToken cancellationToken)
    {
        var proposal =
            await _proposalRepository.Table
                .Include(x => x.Resumes)
                .FirstOrDefaultAsync(x => x.Id == request.ProposalId, cancellationToken: cancellationToken);

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

        if (request.UserRoles.IsNullOrEmpty() || !request.UserRoles.Contains(Enums.RoleTypes.Admin.ToString()))
        {
            if (!proposal.ShowContacts)
            {
                HideContacts(resume);
            }

            if (!proposal.ShowCompanyNames)
            {
                HideCompanyNames(resume);
            }

            if (proposal.IsIncognito)
            {
                HideNames(resume);
            }
        }

        return new ProposalResumeResult
        {
            ResumeTemplateId = proposal.ResumeTemplateId,
            ShowLogo = proposal.ShowLogo,
            Resume = resume
        };
    }

    private static void HideNames(ResumeResult resume)
    {
        resume.FirstName = resume.FirstName;
        resume.LastName = resume.LastName[0].ToString();
    }

    private static void HideCompanyNames(ResumeResult resume)
    {
        foreach (var experience in resume.Experiences)
        {
            experience.Company = null;
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