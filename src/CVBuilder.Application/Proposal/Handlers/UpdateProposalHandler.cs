using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;
using Models.Entities;


public class UpdateProposalHandler : IRequestHandler<UpdateProposalCommand, ProposalResult>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<Proposal, int> _proposalRepository;
    private readonly IShortUrlService _shortUrlService;

    public UpdateProposalHandler(IMapper mapper, IRepository<Proposal, int> proposalRepository, IMediator mediator, IShortUrlService shortUrlService)
    {
        _mapper = mapper;
        _proposalRepository = proposalRepository;
        _mediator = mediator;
        _shortUrlService = shortUrlService;
    }

    public async Task<ProposalResult> Handle(UpdateProposalCommand request, CancellationToken cancellationToken)
    {
        var proposal = _mapper.Map<Proposal>(request);
        var proposalDb = await _proposalRepository.Table
            .Include(x => x.Resumes)
            .ThenInclude(x=>x.ShortUrl)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        
        if (proposalDb == null)
        {
            throw new NotFoundException("Proposal not found");
        }

        MapResumes(proposalDb, proposal);
        UpdateProposal(proposalDb, proposal);
        RemoveDuplicate(proposalDb);
        proposalDb = await _proposalRepository.UpdateAsync(proposalDb);
        var result = await _mediator.Send(new GetProposalByIdQuery {Id = proposalDb.Id}, cancellationToken);
        return result;
    }

    private void MapResumes(Proposal proposalDb, Proposal proposal)
    {
        foreach (var resume in proposal.Resumes)
        {
            var resumeDb = proposalDb.Resumes.FirstOrDefault(x => x.Id == resume.Id);
            if (resumeDb != null)
            {
                resumeDb.StatusResume = resume.StatusResume;
                resumeDb.ShortUrl ??= new ShortUrl
                {
                    Url = _shortUrlService.GenerateShortUrl()
                };
            }
            else
            {
                var newResume = resume;
                newResume.ShortUrl = new ShortUrl()
                {
                    Url = _shortUrlService.GenerateShortUrl()
                };
                proposalDb.Resumes.Add(newResume);
            }
        }
    }

    private void RemoveDuplicate(Proposal proposalDto)
    {
        proposalDto.Resumes = proposalDto.Resumes
            .GroupBy(x => x.ResumeId)
            .Select(y => y.First())
            .ToList();
    }


    private void UpdateProposal(Proposal proposalDto, Proposal proposal)
    {
        proposalDto.UpdatedAt = DateTime.UtcNow;
        proposalDto.ResumeTemplateId = proposal.ResumeTemplateId == 0 ? 1 : proposal.ResumeTemplateId;
        proposalDto.ShowLogo = proposal.ShowLogo;
        proposalDto.ShowContacts = proposal.ShowContacts;
        proposalDto.ShowCompanyNames = proposal.ShowCompanyNames;
        proposalDto.StatusProposal = proposal.StatusProposal;
        proposalDto.ProposalName = proposal.ProposalName;
        proposalDto.ClientId = proposal.ClientId;
        proposalDto.Resumes = proposal.Resumes;
    }
}