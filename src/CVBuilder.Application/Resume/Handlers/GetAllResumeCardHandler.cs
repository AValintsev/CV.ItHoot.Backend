using System;
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
using Microsoft.IdentityModel.Tokens;

namespace CVBuilder.Application.Resume.Handlers
{
    using Models.Entities;

    public class GetAllCvCardHandler : IRequestHandler<GetAllResumeCardQueries, (int, List<ResumeCardResult>)>
    {
        private readonly IDeletableRepository<Resume, int> _cvRepository;
        private readonly IRepository<Models.User, int> _userRepository;
        private readonly IMapper _mapper;

        public GetAllCvCardHandler(IDeletableRepository<Resume, int> cvRepository, IMapper mapper,
            IRepository<Models.User, int> userRepository)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
            _userRepository = userRepository;
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
                query = query.Where(x => !x.DeletedAt.HasValue).OrderBy(x => x.DeletedAt);
            }

            query = query.Include(x => x.Position)
                .Include(x => x.ProposalResumes)
                .ThenInclude(x => x.Proposal)
                .ThenInclude(x => x.Client)
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

            query = FilterQuery(query, request.Positions, request.Skills,request.Clients);

            totalCount = await query.CountAsync(cancellationToken: cancellationToken);

            query = SortQuery(query, request.Order, request.Sort);

            var page = request.Page;
            if (page.HasValue)
            {
                page -= 1;
            }

            query = query.Skip(page.GetValueOrDefault() * request.PageSize.GetValueOrDefault());

            if (request.PageSize != null)
                query = query.Take(request.PageSize.Value);

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
                    case "salaryRate":
                    {
                        query = order == "desc"
                            ? query.OrderByDescending(r => r.SalaryRate)
                            : query.OrderBy(r => r.SalaryRate);
                    }
                        break;
                    default:
                        return query;
                }
            }

            return query;
        }

        private static IQueryable<Resume> FilterQuery(IQueryable<Resume> query, List<int> positions, List<int> skills, List<int> clients)
        {
            if (!positions.IsNullOrEmpty())
            {
                query = query.Where(r => r.PositionId.HasValue && positions.Contains(r.PositionId.Value));
            }

            if (!skills.IsNullOrEmpty())
            {
                foreach (var skillId in skills)
                {
                    query = query.Where(r => r.LevelSkills.Any(ls => ls.SkillId == skillId));
                }
            }

            if (!clients.IsNullOrEmpty())
            {
                foreach (var clientId in clients)
                {
                    query = query.Where(r => r.ProposalResumes.Any(x=>x.Proposal.ClientId == clientId));
                }
            }
            return query;
        }
    }
}