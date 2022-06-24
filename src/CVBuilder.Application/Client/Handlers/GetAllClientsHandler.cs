using AutoMapper;
using CVBuilder.Application.Client.Queries;
using CVBuilder.Application.Client.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CVBuilder.Application.Client.Handlers
{
    public class GetAllClientsHandler : IRequestHandler<GetAllClientsQueries, (int, List<ClientListItemResponse>)>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Models.User, int> _userRepository;
        public GetAllClientsHandler(IMapper mapper, IRepository<Models.User, int> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }


        public async Task<(int, List<ClientListItemResponse>)> Handle(GetAllClientsQueries request, CancellationToken cancellationToken)
        {
            var totalCount = 0;

            var query = _userRepository.Table;

            query = query.Where(u => u.Roles.Any(r => r.NormalizedName.Contains("CLIENT")));
            query = query.Include(u => u.ClientProposals);

            SearchByTerm(ref query, request.Term);

            totalCount = await query.CountAsync();

            query = query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            SortClients(ref query, request.Order, request.Sort);

            var resultList = await query.ToListAsync();

            var result = _mapper.Map<List<ClientListItemResponse>>(resultList);
            return (totalCount, result);
        }

        private static void SearchByTerm(ref IQueryable<Models.User> query, string term)
        {
            if (!string.IsNullOrWhiteSpace(term))
            {
                var lowerTerm = term.ToLower();
                query = query.Where(u => (u.FirstName != null && u.FirstName.ToLower().Contains(lowerTerm))
                                         || (u.LastName != null && u.LastName.ToLower().Contains(lowerTerm))
                                         || (u.Email != null && u.Email.ToLower().Contains(lowerTerm))
                                         || (u.PhoneNumber != null && u.PhoneNumber.ToLower().Contains(lowerTerm))
                                         );
            }
        }

        private static void SortClients(ref IQueryable<Models.User> query, string sortDirections, string columnName)
        {
            if (!string.IsNullOrWhiteSpace(sortDirections) && !string.IsNullOrWhiteSpace(columnName))
            {
                switch (columnName)
                {
                    case "fullName":
                        {
                            query = sortDirections == "desc"
                                ? query.OrderByDescending(r => r.FirstName).ThenByDescending(r => r.LastName)
                                : query.OrderBy(r => r.FirstName).ThenBy(r => r.LastName);
                        }
                        break;
                    case "email":
                        {
                            query = sortDirections == "desc"
                                ? query.OrderBy(u => u.Email)
                                : query.OrderByDescending(u => u.Email);
                        }
                        break;
                    case "phoneNumber":
                        {
                            query = sortDirections == "desc"
                                ? query.OrderBy(u => u.PhoneNumber)
                                : query.OrderByDescending(u => u.PhoneNumber);
                        }
                        break;
                    case "site":
                        {
                            query = sortDirections == "desc" 
                                ? query.OrderBy(u => u.Site) 
                                : query.OrderByDescending(u => u.Site);
                        }
                        break;
                    case "companyName":
                        {
                            query = sortDirections == "desc" ? query.OrderBy(u => u.CompanyName) 
                                : query.OrderByDescending(u => u.CompanyName);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}