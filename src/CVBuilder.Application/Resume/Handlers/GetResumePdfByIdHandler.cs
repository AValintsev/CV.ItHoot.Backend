using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Services.Interfaces;
using MediatR;

namespace CVBuilder.Application.Resume.Handlers;


public class GetResumePdfByIdHandler : IRequestHandler<GetPdfByIdQueries, Stream>
{
    private readonly IMediator _mediator;
    private readonly IPdfPrinter _pdfPrinter;
    public GetResumePdfByIdHandler(IMediator mediator, IPdfPrinter pdfPrinter)
    {
        _mediator = mediator;
        _pdfPrinter = pdfPrinter;
    }

    public async Task<Stream> Handle(GetPdfByIdQueries request, CancellationToken cancellationToken)
    {
        var command = new GetResumeHtmlByIdQuery()
        {
            ResumeId = request.ResumeId,
            UserId = request.UserId,
            UserRoles = request.UserRoles,
            PrintFooter = PrintFooter.ForPdf
        };

        var html = await _mediator.Send(command, cancellationToken);
        var stream = await _pdfPrinter.PrintPdfAsync(html);
        return stream;
    }
}