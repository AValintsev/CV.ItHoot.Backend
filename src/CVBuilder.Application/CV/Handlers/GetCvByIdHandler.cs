using AutoMapper;
using CVBuilder.Application.CV.Queries;
using CVBuilder.Application.CV.Responses;
using CVBuilder.Application.CV.Responses.CvResponses;
using CVBuilder.Application.Extensions;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CVBuilder.Application.CV.Handlers
{
    public class GetCvByIdHandler : IRequestHandler<GetCvByIdQueries, CvResult>
    {
        private readonly IRepository<Cv, int> _cvRepository;
        private readonly IMapper _mapper;
        public GetCvByIdHandler(
            IRepository<Cv, int> cvRepository,
            IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<CvResult> Handle(GetCvByIdQueries request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.Table
               .Include(x => x.Educations)
               .Include(x => x.Experiences)
               .Include(x => x.LevelSkills)
                    .ThenInclude(l => l.Skill)
               .Include(x => x.LevelLanguages)
                    .ThenInclude(l => l.UserLanguage)
               .Include(x => x.Files)
               .Where(x => x.Id == request.Id)
               .FirstOrDefaultAsync();

            if (cv == null)
            {
                throw ValidationException.Build(nameof(request.Id), "Not Found");
            }

            var cvResault = _mapper.Map<CvResult>(cv);


            return cvResault;
        }
    }
}
