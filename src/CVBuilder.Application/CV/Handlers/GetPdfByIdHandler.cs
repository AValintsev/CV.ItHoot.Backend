using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.CV.Queries;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using PuppeteerSharp;

namespace CVBuilder.Application.CV.Handlers
{
    public class GetPdfByIdHandler : IRequestHandler<GetPdfByIdQueries, Stream>
    {
        private readonly IRepository<Cv, int> _cvRepository;

        public GetPdfByIdHandler(IRepository<Cv, int> cvRepository)
        {
            _cvRepository = cvRepository;
        }

        public async Task<Stream> Handle(GetPdfByIdQueries request, CancellationToken cancellationToken)
        {
            var cv = _cvRepository.GetByIdAsync(request.ResumeId);
            if (cv == null)
                throw new NullReferenceException("Resume not found");
            var array = new[]
            {
                ""
            };
            var downloadsFolder = Path.GetTempPath();
            using var browserFetcher = new BrowserFetcher(new BrowserFetcherOptions
            {
                Path = downloadsFolder
            });
            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions {Headless = true,
                ExecutablePath = browserFetcher.RevisionInfo(BrowserFetcher.DefaultChromiumRevision).ExecutablePath
            });
            await using var page = await browser.NewPageAsync();
            await page.GoToAsync("https://tester-lamvb6we0-sominola.vercel.app");
            await page.EvaluateExpressionHandleAsync("document.fonts.ready");
            await page.EvaluateExpressionAsync($"window.localStorage.setItem('JWT_TOKEN', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJnaXZlbl9uYW1lIjoiICIsImVtYWlsIjoibnNhdmNodWsyMjRAZ21haWwuY29tIiwianRpIjoiNTVkNzMxMTQtMGVhMi00Y2MwLWE4ZjItNmVjOWRhNDk1NmExIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiYTJmYmRlMTUtZTRiNS00ZmUwLWI3NzctMGIxM2NlYWNhNGU3IiwibmFtZWlkIjoiMiIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTY1MjY0MDU5MywiZXhwIjoxNjYxMjg0MTkzLCJpYXQiOjE2NTI2NDA1OTN9.j60cyTOSjehhfClc_h7WOzIY-eMZjBWX5MS1ckyVqCo');");
            await page.GoToAsync($"https://tester-lamvb6we0-sominola.vercel.app/home/cv/{request.ResumeId}");
            await Task.Delay(2000, cancellationToken);
            await page.EvaluateExpressionAsync(
                @"var doc = document.getElementById(""doc"");document.body.innerHTML = '';document.body.appendChild(doc);");

            var file = await page.PdfStreamAsync(new PdfOptions
            {
                PrintBackground = true,
                Height = 1415
            });
            return file;
        }
    }
}