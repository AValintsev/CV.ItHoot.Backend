using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Repository;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CVBuilder.Application.Resume.Handlers
{
    public class GetDocxTemplateByIdHandler : IRequestHandler<GetDocxTemplateByIdQueries, Stream>
    {
        private readonly IRepository<Models.Entities.ResumeTemplate, int> _templateRepository;

        public GetDocxTemplateByIdHandler(IRepository<Models.Entities.ResumeTemplate, int> templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<Stream> Handle(GetDocxTemplateByIdQueries request, CancellationToken cancellationToken)
        {
            var template = await _templateRepository.GetByIdAsync(request.TemplateId);

            if (template == null || template.Docx == null)
                throw new NotFoundException("Template not found");

            Stream stream = new MemoryStream(template.Docx);

            return stream;
        }
    }
}
