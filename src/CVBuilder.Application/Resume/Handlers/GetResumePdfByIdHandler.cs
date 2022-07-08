using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Core.Settings;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Services.Interfaces;
using CVBuilder.Repository;
using MediatR;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace CVBuilder.Application.Resume.Handlers;
using Models.Entities;

public class GetResumePdfByIdHandler : IRequestHandler<GetPdfByIdQueries, Stream>
{
    private readonly IBrowserPdfPrinter _browserPdfPrinter;
    private readonly IRepository<Resume, int> _resumeRepository;
    private readonly AppSettings _appSettings;
    public GetResumePdfByIdHandler(IBrowserPdfPrinter browserPdfPrinter, IRepository<Resume, int> resumeRepository, AppSettings appSettings)
    {
        _browserPdfPrinter = browserPdfPrinter;
        _resumeRepository = resumeRepository;
        _appSettings = appSettings;
    }

    public async Task<Stream> Handle(GetPdfByIdQueries request, CancellationToken cancellationToken)
    {
        var resume = await _resumeRepository.GetByIdAsync(request.ResumeId);
        if (resume == null)
        {
            throw new NotFoundException("Resume not found");
        }
        await using var page = await _browserPdfPrinter.LoadPageAsync($"{_appSettings.FrontendUrl}/resume/{request.ResumeId}", request.JwtToken);
        var stream = await _browserPdfPrinter.PrintPdfAsync();
        return stream;
    }
}