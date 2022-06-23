using AutoMapper;
using CVBuilder.Application.Client.Queries;
using CVBuilder.Application.Client.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CVBuilder.Application.Client.Handlers
{
    public class GetAllClientsHandler : IRequestHandler<GetAllClientsQueries, (int, List<ClientListItemResponse>)>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Models.User> _userManager;
        public GetAllClientsHandler(IMapper mapper, UserManager<Models.User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }


        public async Task<(int, List<ClientListItemResponse>)> Handle(GetAllClientsQueries request, CancellationToken cancellationToken)
        {
            var allClient = await _userManager.GetUsersInRoleAsync("Client");
            var totalCount = 0;

            SearchByTerm(ref allClient, request.Term);

            totalCount = allClient.Count();

            var resultList = allClient
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            SortClients(ref resultList, request.Order, request.Sort);

            var result = _mapper.Map<List<ClientListItemResponse>>(resultList);
            return (totalCount, result);
        }

        private static void SearchByTerm(ref IList<Models.User> list, string term)
        {
            if (!string.IsNullOrWhiteSpace(term))
            {
                var lowerTerm = term.ToLower();
                list = list.Where(u => (u.FullName != null && u.FullName.ToLower().Contains(lowerTerm))
                                       || (u.Email != null && u.Email.ToLower().Contains(lowerTerm))
                                       || (u.PhoneNumber != null && u.PhoneNumber.ToLower().Contains(lowerTerm))
                ).ToList();
            }
        }

        private static void SortClients(ref List<Models.User> list, string sortDirections, string columnName)
        {
            if (!string.IsNullOrWhiteSpace(sortDirections) && !string.IsNullOrWhiteSpace(columnName))
            {
                switch (columnName)
                {
                    case "fullName":
                        {
                            list = sortDirections == "desc"
                                ? list.OrderBy(u => u.FullName).ToList()
                                : list.OrderByDescending(u => u.FullName).ToList();
                        }
                        break;
                    case "email":
                        {
                            list = sortDirections == "desc"
                                ? list.OrderBy(u => u.Email).ToList()
                                : list.OrderByDescending(u => u.Email).ToList();
                        }
                        break;
                    case "phoneNumber":
                        {
                            list = sortDirections == "desc"
                                ? list.OrderBy(u => u.PhoneNumber).ToList()
                                : list.OrderByDescending(u => u.PhoneNumber).ToList();
                        }
                        break;
                    case "site":
                        {
                            //list = sortDirections == "desc" ? list.OrderBy(u => u.Site).ToList() : list.OrderByDescending(u => u.Site).ToList();
                        }
                        break;
                    case "companyName":
                        {
                            //list = sortDirections == "desc" ? list.OrderBy(u => u.CompanyName).ToList() : list.OrderByDescending(u => u.CompanyName).ToList();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}