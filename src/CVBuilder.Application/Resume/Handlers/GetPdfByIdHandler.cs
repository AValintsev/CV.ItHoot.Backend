using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Repository;
using MediatR;
using PuppeteerSharp;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

public class GetPdfByIdHandler : IRequestHandler<GetPdfByIdQueries, Stream>
{
    private readonly IRepository<Resume, int> _cvRepository;
    private readonly BrowserExtension _browserExtension;

    public GetPdfByIdHandler(IRepository<Resume, int> cvRepository, BrowserExtension browserExtension)
    {
        _cvRepository = cvRepository;
        _browserExtension = browserExtension;
    }

    public async Task<Stream> Handle(GetPdfByIdQueries request, CancellationToken cancellationToken)
    {
        // var cv = await _cvRepository.GetByIdAsync(request.ResumeId);
        // if (cv == null)
        //     throw new NullReferenceException("Resume not found");

        var browser = _browserExtension.Browser;
        await using var page = await browser.NewPageAsync();
        await page.EvaluateExpressionOnNewDocumentAsync(
            $"window.localStorage.setItem('JWT_TOKEN', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJnaXZlbl9uYW1lIjoiICIsImVtYWlsIjoibnNhdmNodWsyMjRAZ21haWwuY29tIiwianRpIjoiNTVkNzMxMTQtMGVhMi00Y2MwLWE4ZjItNmVjOWRhNDk1NmExIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiYTJmYmRlMTUtZTRiNS00ZmUwLWI3NzctMGIxM2NlYWNhNGU3IiwibmFtZWlkIjoiMiIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTY1MjY0MDU5MywiZXhwIjoxNjYxMjg0MTkzLCJpYXQiOjE2NTI2NDA1OTN9.j60cyTOSjehhfClc_h7WOzIY-eMZjBWX5MS1ckyVqCo');");
        await page.GoToAsync($"https://cvbuilder-front.vercel.app/home/cv/{request.ResumeId}");
        await Task.Delay(2000, cancellationToken);
        await page.EvaluateExpressionAsync(
            @"var doc = document.getElementById(""doc"");document.body.innerHTML = '';document.body.appendChild(doc);");

        var file = await page.PdfStreamAsync(new PdfOptions
        {
            PrintBackground = true,
            Height = 1450
        });
        return file;
    }
}