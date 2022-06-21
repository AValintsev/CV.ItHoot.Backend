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
        var pdfGenerator = new BrowserPdfGenerator(page);
        await pdfGenerator.SetJwtTokenAsync(request.JwtToken);
        await pdfGenerator.LoadPageAsync($"https://cvbuilder-front.vercel.app/resume/{request.ResumeId}", "#resume-loaded");
        var stream = await pdfGenerator.GetStreamPdfAsync();

        return stream;
    }
}