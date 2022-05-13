using System;
using AutoMapper;
using CVBuilder.Application.CV.Queries;
using CVBuilder.Application.Extensions;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.CV.Responses.CvResponse;

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
               .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (cv == null)
            {
                throw ValidationException.Build(nameof(request.Id), "Not Found");
            }
            
            

            var cvResult = _mapper.Map<CvResult>(cv);
            var inArray = cv.Files?.FirstOrDefault()?.Data;
            if (inArray != null)
                cvResult.Picture = Convert.ToBase64String(inArray);

            return cvResult;
        }
    }
}
