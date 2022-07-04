using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Services;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

public class GetResumeHtmlByIdHandler : IRequestHandler<GetResumeHtmlByIdQuery, string>
{
    private readonly IRepository<ResumeTemplate, int> _templateRepository;
    private readonly IRepository<Resume, int> _resumeRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetResumeHtmlByIdHandler(IRepository<ResumeTemplate, int> templateRepository, IMapper mapper,
        IRepository<Resume, int> resumeRepository, IMediator mediator)
    {
        _templateRepository = templateRepository;
        _mapper = mapper;
        _resumeRepository = resumeRepository;
        _mediator = mediator;
    }

    public async Task<string> Handle(GetResumeHtmlByIdQuery query, CancellationToken cancellationToken)
    {
        var command = new GetResumeByIdQuery()
        {
            Id = query.ResumeId,
            UserId = query.UserId,
            UserRoles = query.UserRoles
        };

        var resume = await _mediator.Send(command, cancellationToken);

        var template = await _templateRepository.GetByIdAsync(resume.ResumeTemplateId);

        if (template == null)
        {
            throw new NullReferenceException("ResumeTemplate not found");
        }
        
        var printFooter = query.PrintFooter switch
        {
            PrintFooter.Print => true,
            PrintFooter.NotPrint => false,
            PrintFooter.ForHtml => CheckOutForHtml(query),
            PrintFooter.ForPdf => CheckOutForPdf(query),
            _ => false
        };
        
        var builder = new ResumeTemplateBuilder(template.Html);
        var html = await builder.BindTemplateAsync(resume, printFooter);
        return html;
    }

    private static bool CheckOutForPdf(GetResumeHtmlByIdQuery query)
    {
        return true;
    }

    private static bool CheckOutForHtml(GetResumeHtmlByIdQuery query)
    {
        return false;
    }
}