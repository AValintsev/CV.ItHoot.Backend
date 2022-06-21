using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Application.Resume.Services;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

public class GetTemplateByIdHandler : IRequestHandler<GetTemplateByIdQuery, TemplateResult>
{
    private readonly IRepository<ResumeTemplate, int> _templateRepository;
    private readonly IRepository<Resume, int> _resumeRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetTemplateByIdHandler(IRepository<ResumeTemplate, int> templateRepository, IMapper mapper,
        IRepository<Resume, int> resumeRepository, IMediator mediator)
    {
        _templateRepository = templateRepository;
        _mapper = mapper;
        _resumeRepository = resumeRepository;
        _mediator = mediator;
    }

    public async Task<TemplateResult> Handle(GetTemplateByIdQuery request, CancellationToken cancellationToken)
    {
        var template = await _templateRepository.GetByIdAsync(request.Id);

        if (template == null)
        {
            throw new NotFoundException("Template not found");
        }

        var model = _mapper.Map<TemplateResult>(template);

        return model;
    }
}