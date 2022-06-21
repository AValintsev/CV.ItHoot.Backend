using System.IO;
using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class CreateTemplateCommand : IRequest<TemplateResult>
{
    public Stream HtmlStream { get; set; }
}