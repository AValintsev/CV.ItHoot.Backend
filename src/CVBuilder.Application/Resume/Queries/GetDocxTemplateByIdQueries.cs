using System.IO;
using MediatR;

namespace CVBuilder.Application.Resume.Queries
{
    public class GetDocxTemplateByIdQueries : IRequest<Stream>
    {
        public int TemplateId { get; private set; }

        public GetDocxTemplateByIdQueries(int templateId)
        {
            TemplateId = templateId;
        }
    }
}