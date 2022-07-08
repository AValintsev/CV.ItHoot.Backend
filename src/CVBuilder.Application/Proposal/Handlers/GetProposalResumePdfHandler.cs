using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Core.Settings;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Resume.Services.Interfaces;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

using Models.Entities;
public class GetProposalResumePdfHandler : IRequestHandler<GetProposalResumePdfQuery, Stream>
{
    private readonly IRepository<Proposal,int> _proposalRepository;
    private readonly IBrowserPdfPrinter _browserPdfPrinter;
    private readonly AppSettings _appSettings;

    public GetProposalResumePdfHandler(IRepository<Proposal, int> proposalRepository, IBrowserPdfPrinter browserPdfPrinter, AppSettings appSettings)
    {
        _proposalRepository = proposalRepository;
        _browserPdfPrinter = browserPdfPrinter;
        _appSettings = appSettings;
    }

    public async Task<Stream> Handle(GetProposalResumePdfQuery request, CancellationToken cancellationToken)
    {
        var proposal = await _proposalRepository.Table
            .Include(x => x.Resumes)
            .FirstOrDefaultAsync(x => x.Id == request.ProposalId, cancellationToken: cancellationToken);

        if (proposal == null)
        {
            throw new NotFoundException("Proposal not found");
        }

        var resume = proposal.Resumes.FirstOrDefault(x => x.Id == request.ProposalResumeId);


        if (resume == null)
        {
            throw new NotFoundException("Resume not found");
        }
       
       
        
        await using var page = await _browserPdfPrinter.LoadPageAsync($"{_appSettings.FrontendUrl}/proposals/{request.ProposalId}/resume/{request.ProposalResumeId}", request.JwtToken);
        var stream = await _browserPdfPrinter.PrintPdfAsync();
        return stream;
    }
}