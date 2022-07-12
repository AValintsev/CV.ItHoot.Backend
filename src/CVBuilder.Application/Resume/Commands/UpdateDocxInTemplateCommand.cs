using System.IO;
using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class UpdateDocxInTemplateCommand : IRequest<TemplateResult>
{
    public int TemplateId { get; private set; }
    public byte[] Data { get; private set; }

    public UpdateDocxInTemplateCommand(int templateId, byte[] data)
    {
        TemplateId = templateId;
        Data = data;
    }
}