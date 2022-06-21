using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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

    public async Task<string> Handle(GetResumeHtmlByIdQuery request, CancellationToken cancellationToken)
    {
        var command = new GetResumeByIdQuery()
        {
            Id = request.Id,
            UserId = request.UserId,
            UserRoles = request.UserRoles
        };

        var resume = await _mediator.Send(command, cancellationToken);

        var template = await _templateRepository.GetByIdAsync(resume.ResumeTemplateId);

        if (template == null)
        {
            // throw new NullReferenceException("ResumeTemplate not found");
            template = await _templateRepository.GetByIdAsync(4);
        }

        var builder = new ResumeTemplateBuilder(template.Html);
        var html = await builder.BindTemplateAsync(resume);
        return html;
    }
}