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
            CancellationToken cancellationToken)
        {
            var result = new List<Resume>();
            var totalCount = 0;

            var query = _cvRepository.TableWithDeleted;

            if (request.IsArchive)
            {
                query = query.Where(x => x.DeletedAt.HasValue);
            }
            else
            {
                query = query.Where(x => !x.DeletedAt.HasValue);
            }

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

            query = SearchByTerm(query, request.Term);

            query = FilterQuery(query, request.Positions, request.Skills);

            totalCount = await query.CountAsync(cancellationToken: cancellationToken);

            query = query.Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            query = SortQuery(query, request.Order, request.Sort);

            result = await query.ToListAsync(cancellationToken: cancellationToken);

            return (totalCount, _mapper.Map<List<ResumeCardResult>>(result));
        }

        private static IQueryable<Resume> SearchByTerm(IQueryable<Resume> query, string searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.ToLower();
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
                                         || r.RequiredPosition.ToLower().Contains(term)
                                         || r.Position.PositionName.ToLower().Contains(term));
            }

            return query;
        }

        private static IQueryable<Resume> SortQuery(IQueryable<Resume> query, string order, string sort)
        {
            if (!string.IsNullOrWhiteSpace(sort) && !string.IsNullOrWhiteSpace(order))
            {
                switch (sort)
                {
                    case "name":
                    {
                        query = order == "desc"
                            ? query.OrderByDescending(r => r.FirstName).ThenByDescending(r => r.LastName)
                            : query.OrderBy(r => r.FirstName).ThenBy(r => r.LastName);
                    }
                        break;
                    case "position":
                    {
                        query = order == "desc"
                            ? query.OrderByDescending(r => r.Position.PositionName)
                            : query.OrderBy(r => r.Position.PositionName);
                    }
                        break;
                    default:
                        query.OrderBy(r => r.FirstName).ThenBy(r => r.LastName);
                        break;
                }
            }

            return query;
        }

        private static IQueryable<Resume> FilterQuery(IQueryable<Resume> query, List<int> positions, List<int> skills)
        {
            if (positions != null && positions.Count > 0)
            {
                query = query.Where(r => r.PositionId.HasValue && positions.Contains(r.PositionId.Value));
            }

            if (skills != null && skills.Count > 0)
            {
                foreach (var skillId in skills)
                {
                    query = query.Where(r => r.LevelSkills.Any(ls => ls.SkillId == skillId));
                }
            }

            return query;
        }
    }
}