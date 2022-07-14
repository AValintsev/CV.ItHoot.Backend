using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Proposal.Queries;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CVBuilder.Application.Proposal.Handlers;

using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Services.Interfaces;
public class GetProposalResumeDocxHandler : IRequestHandler<GetProposalResumeDocxQuery, Stream>
{
    private readonly IDocxBuilder _docxBuilder;
    private readonly IMediator _mediator;

    public GetProposalResumeDocxHandler(IDocxBuilder docxBuilder, IMediator mediator)
    {
        _docxBuilder = docxBuilder;
        _mediator = mediator;
    }

    public async Task<Stream> Handle(GetProposalResumeDocxQuery request, CancellationToken cancellationToken)
    {

        var query = new GetProposalResumeQuery
        {
            ProposalId = request.ProposalId,
            ProposalResumeId = request.ProposalResumeId,
            UserId = request.UserId,
            UserRoles = request.UserRoles,
        };

        var resume = await _mediator.Send(query);

        var docxTemplate = await _mediator.Send(new GetDocxTemplateByIdQueries(resume.ResumeTemplateId));

        if (docxTemplate == null || docxTemplate.Length == 0)
        {
            throw new NotFoundException("Docx template to resume not found");
        }

        byte[] buffer = new byte[16 * 1024];
        using MemoryStream ms = new MemoryStream();
        int read;
        while ((read = docxTemplate.Read(buffer, 0, buffer.Length)) > 0)
        {
            ms.Write(buffer, 0, read);
        }

        var stream = await _docxBuilder.BindTemplateAsync(resume.Resume, ms.ToArray(), resume.ShowLogo);

        return stream;
    }
}