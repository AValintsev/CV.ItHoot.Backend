using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Extensions;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses.CvResponse;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers
{
    public class GetCvByIdHandler : IRequestHandler<GetResumeByIdQueries, ResumeResult>
    {
        private readonly IRepository<Models.Entities.Resume, int> _cvRepository;
        private readonly IMapper _mapper;
        public GetCvByIdHandler(
            IRepository<Models.Entities.Resume, int> cvRepository,
            IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<ResumeResult> Handle(GetResumeByIdQueries request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.Table
                .Include(x=>x.Image)
               .Include(x => x.Educations)
               .Include(x => x.Experiences)
               .Include(x => x.LevelSkills)
                    .ThenInclude(l => l.Skill)
                .Include(x=>x.Position)
               .Include(x => x.LevelLanguages)
                    .ThenInclude(l => l.Language)
               .Where(x => x.Id == request.Id)
               .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (cv == null)
            {
                throw ValidationException.Build(nameof(request.Id), "Not Found");
            }
            

            var cvResult = _mapper.Map<ResumeResult>(cv);
            return cvResult;
        }
    }
}
