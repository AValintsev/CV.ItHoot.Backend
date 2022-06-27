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
    public class GetAllCvCardHandler : IRequestHandler<GetAllResumeCardQueries, (int, List<ResumeCardResult>)>
    {
        private readonly IDeletableRepository<Resume, int> _cvRepository;
        private readonly IMapper _mapper;

        public GetAllCvCardHandler(IDeletableRepository<Resume, int> cvRepository, IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<(int, List<ResumeCardResult>)> Handle(GetAllResumeCardQueries request,
            CancellationToken cancellationToken){
            var result = new List<Resume>();
            var totalCount = 0;

            var query = request.UserRoles.Contains("Admin") ? _cvRepository.TableWithDeleted : _cvRepository.Table;

            query = query.Include(x => x.Position)
                         .Include(x => x.LevelSkills)
                         .ThenInclude(x => x.Skill);

            if (request.UserRoles.Contains("HR"))
            {
                query = query.Where(x => x.IsDraft == false);
            }
            else if (request.UserRoles.Contains("Admin"))
            {
            }
            else if (request.UserRoles.Contains("User"))
            {
                query = query.Where(x => x.CreatedUserId == request.UserId);
            }

            if (!string.IsNullOrWhiteSpace(request.Term))
            {
                var term = request.Term.ToLower();
                query = query.Where(r => r.FirstName.ToLower().Contains(term)
                                               || r.LastName.ToLower().Contains(term)
                                               || r.AboutMe.ToLower().Contains(term)
                                               || r.Birthdate.ToLower().Contains(term)
                                               || r.City.ToLower().Contains(term)
                                               || r.Code.ToLower().Contains(term)
                                               || r.Country.ToLower().Contains(term)
                                               || r.Email.ToLower().Contains(term)
                                               || r.ResumeName.ToLower().Contains(term)
                                               || r.Site.ToLower().Contains(term)
                                               || r.Phone.ToLower().Contains(term)
                                               || r.Street.ToLower().Contains(term)
                                               || r.RequiredPosition.ToLower().Contains(term));
            }

            if (request.Positions != null && request.Positions.Count > 0)
            {
                query = query.Where(r => r.PositionId.HasValue && request.Positions.Contains(r.PositionId.Value));
            }

            if (request.Skills != null && request.Skills.Count > 0)
            {
                foreach (var skillId in request.Skills)
                {
                    query = query.Where(r => r.LevelSkills.Any(ls => ls.SkillId == skillId));
                }
            }
            
            if (request.Skills != null && request.Skills.Count > 0)
            {
                foreach (var skillId in request.Skills)
                {
                    query = query.Where(r => r.LevelSkills.Any(ls => ls.SkillId == skillId));
                }
            }

            totalCount = await query.CountAsync(cancellationToken: cancellationToken);

            query = query.Skip((request.Page - 1) * request.PageSize)
                         .Take(request.PageSize);
            if (!string.IsNullOrWhiteSpace(request.Sort) && !string.IsNullOrWhiteSpace(request.Order))
            {
                switch (request.Sort)
                {
                    case "name":
                        {
                            query = request.Order == "desc" ? 
                                query.OrderByDescending(r => r.FirstName).ThenByDescending(r => r.LastName) 
                                : query.OrderBy(r => r.FirstName).ThenBy(r => r.LastName);
                        }
                        break;
                    case "position":
                        {
                            query = request.Order == "desc" ? query.OrderByDescending(r => r.Position.PositionName) : query.OrderBy(r => r.Position.PositionName);
                        }
                        break;
                    default:
                        query.OrderBy(r => r.FirstName).ThenBy(r => r.LastName); 
                        break;
                }
            }


            result = await query.ToListAsync(cancellationToken: cancellationToken);

            return (totalCount, _mapper.Map<List<ResumeCardResult>>(result));
        }
    }
}