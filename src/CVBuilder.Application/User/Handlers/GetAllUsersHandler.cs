using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.User.Queries;
using CVBuilder.Application.User.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.User.Handlers
{
    using Models;

    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, (int,List<SmallUserResult>)>
    {
        private readonly IRepository<User, int> _repositoryUser;
        private readonly IMapper _mapper;

        public GetAllUsersHandler(IRepository<User, int> repositoryUser, IMapper mapper)
        {
            _repositoryUser = repositoryUser;
            _mapper = mapper;
        }

        public async Task<(int,List<SmallUserResult>)> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var query = _repositoryUser.Table;
            query = query.Include(x => x.Roles);
            var count = 0;


            if (!string.IsNullOrWhiteSpace(request.Term))
            {
                var term = request.Term.ToLower();
                query = query.Where(p => p.FirstName.ToLower().Contains(term)
                                         || p.LastName.ToLower().Contains(term)
                                         || p.Email.ToLower().Contains(term));
            }

            count = await query.CountAsync(cancellationToken: cancellationToken);

            var page = request.Page;
            if (page.HasValue)
            {
                page -= 1;
            }

            query = query.Skip(page.GetValueOrDefault() * request.PageSize.GetValueOrDefault());

            if (request.PageSize != null)
                query = query.Take(request.PageSize.Value);

            if (!string.IsNullOrWhiteSpace(request.Sort) && !string.IsNullOrWhiteSpace(request.Order))
            {
                Sort(ref query,request);
            }

            var users = await query.ToListAsync(cancellationToken: cancellationToken);
            
            var usersResult = _mapper.Map<List<SmallUserResult>>(users);

            return (count,usersResult);
        }

        private void Sort(ref IQueryable<User> query, GetAllUsersQuery request)
        {
            switch (request.Sort)
            {
                case "fullName":
                {
                    query = request.Order == "desc"
                        ? query.OrderByDescending(p => p.FirstName).ThenByDescending(p => p.LastName)
                        : query.OrderBy(p => p.FirstName).ThenBy(p => p.LastName);
                }
                    break;
                case "email":
                {
                    query = request.Order == "desc"
                        ? query.OrderByDescending(p => p.Email)
                        : query.OrderBy(p => p.Email);
                }
                    break;
                case "role":
                {
                    query = request.Order == "desc"
                        ? query.OrderByDescending(p => p.Roles.FirstOrDefault().Name)
                        : query.OrderBy(p => p.Roles.FirstOrDefault().Name);
                }
                    break;
                case "createdAt":
                {
                    query = request.Order == "desc"
                        ? query.OrderByDescending(p => p.CreatedAt)
                        : query.OrderBy(p => p.CreatedAt);
                }
                    break;
            }
        }
    }
}