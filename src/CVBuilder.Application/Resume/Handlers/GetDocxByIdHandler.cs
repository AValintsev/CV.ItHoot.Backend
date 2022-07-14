using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Repository;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CVBuilder.Application.Resume.Responses.CvResponse;

namespace CVBuilder.Application.Resume.Handlers
{
    public class GetDocxByIdHandler : IRequestHandler<GetDocxByIdQueries, Stream>
    {
        private readonly IDeletableRepository<Models.Entities.Resume, int> _resumeRepository;
        private readonly IDocxBuilder _docxBuilder;
        private readonly IMapper _mapper;

        public GetDocxByIdHandler(IDeletableRepository<Models.Entities.Resume, int> resumeRepository,
            IDocxBuilder docxBuilder,
            IMapper mapper)
        {
            _resumeRepository = resumeRepository;
            _docxBuilder = docxBuilder;
            _mapper = mapper;
        }

        public async Task<Stream> Handle(GetDocxByIdQueries request, CancellationToken cancellationToken)
        {
            var query = _resumeRepository.Table
                    .Include(x => x.ResumeTemplate)
                    .Include(x => x.Image)
                    .Include(x => x.Educations)
                    .Include(x => x.Experiences)
                    .Include(x => x.LevelSkills)
                    .ThenInclude(l => l.Skill)
                    .Include(x => x.Position)
                    .Include(x => x.LevelLanguages)
                    .ThenInclude(l => l.Language);

            var resume = await query.FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken: cancellationToken);

            if (resume == null)
                throw new NotFoundException("Resume not found");

            var mappedResume = _mapper.Map<ResumeResult>(resume);

            if (resume.ResumeTemplate.Docx == null)
            {
                throw new NotFoundException("Docx template to resume not found");
            }
            var stream = await _docxBuilder.BindTemplateAsync(mappedResume, resume.ResumeTemplate.Docx);

            return stream;
        }
    }
}
