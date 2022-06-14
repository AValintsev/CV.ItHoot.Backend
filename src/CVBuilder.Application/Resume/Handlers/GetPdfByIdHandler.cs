using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Repository;
using MediatR;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

public class GetPdfByIdHandler : IRequestHandler<GetPdfByIdQueries, Stream>
{
    private readonly IDeletableRepository<Resume, int> _cvRepository;
    private readonly BrowserExtension _browserExtension;

    public GetPdfByIdHandler(IDeletableRepository<Resume, int> cvRepository, BrowserExtension browserExtension)
    {
        _cvRepository = cvRepository;
        _browserExtension = browserExtension;
    }

    public async Task<Stream> Handle(GetPdfByIdQueries request, CancellationToken cancellationToken)
    {
        var resume = await _cvRepository.GetByIdAsync(request.ResumeId);
        if (resume == null)
            throw new NotFoundException("Resume not found");

        var browser = _browserExtension.Browser;
        await using var page = await browser.NewPageAsync();
        await page.EvaluateExpressionOnNewDocumentAsync(
            $"window.localStorage.setItem('JWT_TOKEN', '{request.JwtToken}');");
        await page.GoToAsync($"https://cvbuilder-front.vercel.app/home/cv/{request.ResumeId}");
        await page.WaitForSelectorAsync("#doc", new WaitForSelectorOptions()
        {
            Visible = true,
            Timeout = 5000
        });
        
        await page.EvaluateExpressionAsync(
        @"var doc = document.getElementById(""doc"");document.body.innerHTML = '';document.body.appendChild(doc);");
        await page.EmulateMediaTypeAsync(MediaType.Print);
        var file = await page.PdfStreamAsync(new PdfOptions
        {
            PrintBackground = true,
            Format = PaperFormat.A4,
        });
        return file;
    }
}