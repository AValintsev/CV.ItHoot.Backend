using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Resume.Handlers;

public class UpdateTemplateHandler : IRequestHandler<UpdateTemplateCommand, TemplateResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ResumeTemplate, int> _templateRepository;

    public UpdateTemplateHandler(IRepository<ResumeTemplate, int> templateRepository, IMapper mapper)
    {
        _templateRepository = templateRepository;
        _mapper = mapper;
    }

    public async Task<TemplateResult> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
    {
        string html;
        using var reader = new StreamReader(request.HtmlStream, Encoding.UTF8);
        {
            html = await reader.ReadToEndAsync();
        }

        var template = await _templateRepository.GetByIdAsync(request.Id);

        template.TemplateName = request.TemplateName;
        template.Html = html;

        template = await _templateRepository.UpdateAsync(template);

        var model = _mapper.Map<TemplateResult>(template);
        return model;
    }
}