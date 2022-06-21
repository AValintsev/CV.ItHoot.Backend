using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Repository;
using MediatR;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace CVBuilder.Application.Proposal.Handlers;

public class GetProposalResumePdfHandler : IRequestHandler<GetProposalResumePdfQuery, Stream>
{
    private readonly BrowserExtension _browserExtension;


    public GetProposalResumePdfHandler(BrowserExtension browserExtension)
    {
        _browserExtension = browserExtension;
    }

    public async Task<Stream> Handle(GetProposalResumePdfQuery request, CancellationToken cancellationToken)
    {
        var browser = _browserExtension.Browser;
        await using var page = await browser.NewPageAsync();
        var pdfGenerator = new BrowserPdfGenerator(page);
        await pdfGenerator.SetJwtTokenAsync(request.JwtToken);
        await pdfGenerator.LoadPageAsync($"https://cvbuilder-front.vercel.app/proposals/{request.ProposalId}/resume/{request.ProposalResumeId}", "#resume-loaded");
        var stream = await pdfGenerator.GetStreamPdfAsync();

        return stream;
    }
}