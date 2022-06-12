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
        if (!string.IsNullOrEmpty(request.JwtToken))
        {
            await page.EvaluateExpressionOnNewDocumentAsync(
                $"window.localStorage.setItem('JWT_TOKEN', '{request.JwtToken}');");
        }
        await page.GoToAsync($"https://cvbuilder-front.vercel.app/proposals/{request.ProposalId}/resume/{request.ProposalResumeId}");
        await page.EmulateMediaTypeAsync(MediaType.Print);
        await page.WaitForSelectorAsync("#doc", new WaitForSelectorOptions()
        {
            Visible = true,
            Timeout = 5000
        });

        
        var file = await page.PdfStreamAsync(new PdfOptions
        {
            PrintBackground = true,
            Format = PaperFormat.A4,
        });
        return file;
    }
}