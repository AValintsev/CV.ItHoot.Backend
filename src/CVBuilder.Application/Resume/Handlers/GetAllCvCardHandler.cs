using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers
{
    using Models.Entities;
    public class GetAllCvCardHandler : IRequestHandler<GetAllResumeCardQueries, List<ResumeCardResult>>
    {
        private readonly IDeletableRepository<Resume, int> _cvRepository;
        private readonly IMapper _mapper;

        public GetAllCvCardHandler(IDeletableRepository<Resume, int> cvRepository, IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<List<ResumeCardResult>> Handle(GetAllResumeCardQueries request,
            CancellationToken cancellationToken)
        {
            var result = new List<Resume>();
            if (request.UserRoles.Contains("HR"))
            {
                result = await _cvRepository.Table
                    .Include(x=>x.Position)
                    .Include(x => x.LevelSkills)
                    .ThenInclude(x => x.Skill)
                    .Where(x => x.IsDraft == false)
                    .ToListAsync(cancellationToken: cancellationToken);
            }
            else if (request.UserRoles.Contains("Admin"))
            {
                result = await _cvRepository.TableWithDeleted
                    .Include(x=>x.Position)
                    .Include(x => x.LevelSkills)
                    .ThenInclude(x => x.Skill)
                    .ToListAsync(cancellationToken: cancellationToken);
            }
            else if (request.UserRoles.Contains("User"))
            {
                result = await _cvRepository.Table
                    .Include(x=>x.Position)
                    .Include(x => x.LevelSkills)
                    .ThenInclude(x => x.Skill)
                    .Where(x => x.CreatedUserId == request.UserId)
                    .ToListAsync(cancellationToken: cancellationToken);
            }


            return _mapper.Map<List<ResumeCardResult>>(result);
        }
    }
}