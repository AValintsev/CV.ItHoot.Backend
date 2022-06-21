using System.IO;
using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class UpdateTemplateCommand : IRequest<TemplateResult>
{
    public int Id { get; set; }
    public string TemplateName { get; set; }
    public Stream HtmlStream { get; set; }
}