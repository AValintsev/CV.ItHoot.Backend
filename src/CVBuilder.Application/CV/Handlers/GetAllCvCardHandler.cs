using AutoMapper;
using CVBuilder.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CVBuilder.Application.CV.Queries;
using CVBuilder.Application.CV.Responses;

namespace CVBuilder.Application.CV.Handlers
{
    public class GetAllCvCardHandler : IRequestHandler<GetAllCvCardQueries, GetAllCvCardResult>
    {
        private readonly IRepository<Cv, int> _cvRepository;
        private readonly IMapper _mapper;

        public GetAllCvCardHandler(IRepository<Cv, int> cvRepository, IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<GetAllCvCardResult> Handle(GetAllCvCardQueries request, CancellationToken cancellationToken)
        {
            var result = new List<Cv>();
            if (request.UserRoles.Contains("HR"))
            {
                result = await _cvRepository.Table
                    .Include(x=>x.LevelSkills)
                    .ThenInclude(x=>x.Skill)
                    .Where(x => x.IsDraft == false)
                    .ToListAsync(cancellationToken: cancellationToken);
            }
            else if (request.UserRoles.Contains("Admin"))
            {
                result = await _cvRepository.Table
                    .Include(x => x.LevelSkills)
                    .ThenInclude(x => x.Skill)
                    .ToListAsync(cancellationToken: cancellationToken);
            }
            else if (request.UserRoles.Contains("User"))
            {
                result = await _cvRepository.Table
                    .Include(x=>x.LevelSkills)
                    .ThenInclude(x=>x.Skill)
                    .Where(x => x.UserId == request.UserId)
                    .ToListAsync(cancellationToken: cancellationToken);
            }

            return new GetAllCvCardResult
            {
                CvCards = _mapper.Map<List<CvCardResult>>(result),
            };
        }
    }
}
