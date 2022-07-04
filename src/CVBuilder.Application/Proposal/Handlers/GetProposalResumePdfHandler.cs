using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Services.Interfaces;
using CVBuilder.Repository;
using MediatR;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace CVBuilder.Application.Proposal.Handlers;

public class GetProposalResumePdfHandler : IRequestHandler<GetProposalResumePdfQuery, Stream>
{
    private readonly IMediator _mediator;
    private readonly IPdfPrinter _pdfPrinter;

    public GetProposalResumePdfHandler(IMediator mediator, IPdfPrinter pdfPrinter)
    {
        _mediator = mediator;
        _pdfPrinter = pdfPrinter;
    }

    public async Task<Stream> Handle(GetProposalResumePdfQuery request, CancellationToken cancellationToken)
    {
        var command = new GetProposalResumeHtmlQuery()
        {
            ProposalId = request.ProposalId,
            ProposalResumeId = request.ProposalResumeId,
            UserRoles = request.UserRoles,
            UserId = request.UserId,
            PrintFooter = PrintFooter.ForPdf
        };

        var html = await _mediator.Send(command, cancellationToken);

        var stream = await _pdfPrinter.PrintPdfAsync(html);

        return stream;
    }
}