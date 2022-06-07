using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Team.Queries;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;

namespace CVBuilder.Application.Team.Handlers;

using Models.Entities;

public class GetTeamResumePdfHandler : IRequestHandler<GetTeamResumePdfQuery, Stream>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<Team, int> _teamRepository;
    private readonly BrowserExtension _browserExtension;


    public GetTeamResumePdfHandler(IMapper mapper, IRepository<Team, int> teamRepository, IMediator mediator,
        BrowserExtension browserExtension)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
        _mediator = mediator;
        _browserExtension = browserExtension;
    }

    public async Task<Stream> Handle(GetTeamResumePdfQuery request, CancellationToken cancellationToken)
    {
        var browser = _browserExtension.Browser;
        await using var page = await browser.NewPageAsync();
        if (!string.IsNullOrEmpty(request.JwtToken))
        {
            await page.EvaluateExpressionOnNewDocumentAsync(
                $"window.localStorage.setItem('JWT_TOKEN', '{request.JwtToken}');");
        }

        await page.GoToAsync($"https://cvbuilder-front.vercel.app/teams/{request.TeamId}/resume/{request.TeamResumeId}");
        await page.WaitForSelectorAsync("#doc", new WaitForSelectorOptions()
        {
            Visible = true,
            Timeout = 5000
        });

        var file = await page.PdfStreamAsync(new PdfOptions
        {
            PrintBackground = true,
            Height = 1250
        });
        return file;
    }
}