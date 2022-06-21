using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;
using Models.Entities;

public class GetAllArchiveProposalsHandler: IRequestHandler<GetAllArchiveProposalsQuery, (int, List<SmallProposalResult>)>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Proposal, int> _proposalRepository;

    public GetAllArchiveProposalsHandler(IMapper mapper, IRepository<Models.Entities.Proposal, int> proposalRepository)
    {
        _mapper = mapper;
        _proposalRepository = proposalRepository;
    }

    public async Task<(int, List<SmallProposalResult>)> Handle(GetAllArchiveProposalsQuery request, CancellationToken cancellationToken)
    {
        var proposals = new List<Proposal>();
        var count = 0;

        var query = _proposalRepository.Table;

        query = query.Where(x => x.StatusProposal == StatusProposal.Done);

        query = query.Include(x => x.Resumes)
                     .Include(x => x.CreatedUser)
                     .Include(x => x.Client);

        if (!string.IsNullOrWhiteSpace(request.Term))
        {
            var term = request.Term.ToLower();
            query = query.Where(p => p.ProposalName.ToLower().Contains(term)
                                     || p.Client.FirstName.ToLower().Contains(term)
                                     || p.Client.LastName.ToLower().Contains(term)
                                     || p.CreatedUser.FirstName.ToLower().Contains(term)
                                     || p.CreatedUser.LastName.ToLower().Contains(term));
        }

        if (request.Clients != null && request.Clients.Count > 0)
        {
            query = query.Where(p => p.ClientId.HasValue && request.Clients.Contains(p.ClientId.Value));
        }

        count = await query.CountAsync(cancellationToken: cancellationToken);

        query = query.Skip((request.Page - 1) * request.PageSize)
                         .Take(request.PageSize);

        if (!string.IsNullOrWhiteSpace(request.Sort) && !string.IsNullOrWhiteSpace(request.Order))
        {
            switch (request.Sort)
            {
                case "proposalName":
                    {
                        query = request.Order == "desc" ? query.OrderByDescending(p => p.ProposalName) : query.OrderBy(p => p.ProposalName);
                    }
                    break;
                case "clientUserName":
                    {
                        query = request.Order == "desc" ? query.OrderByDescending(p => p.Client.FirstName).ThenByDescending(p => p.Client.LastName)
                            : query.OrderBy(p => p.Client.FirstName).ThenBy(p => p.Client.LastName);
                    }
                    break;
                case "proposalSize":
                    {
                        query = request.Order == "desc" ? query.OrderByDescending(p => p.Resumes.Count) : query.OrderBy(p => p.Resumes.Count);
                    }
                    break;
                case "showLogo":
                    {
                        query = request.Order == "desc" ? query.OrderByDescending(p => p.ShowLogo) : query.OrderBy(p => p.ShowLogo);
                    }
                    break;
                case "showContacts":
                    {
                        query = request.Order == "desc" ? query.OrderByDescending(p => p.ShowContacts) : query.OrderBy(p => p.ShowContacts);
                    }
                    break;
                case "lastUpdated":
                    {
                        query = request.Order == "desc" ? query.OrderByDescending(p => p.UpdatedAt) : query.OrderBy(p => p.UpdatedAt);
                    }
                    break;
                case "createdUserName":
                    {
                        query = request.Order == "desc" ? query.OrderByDescending(p => p.CreatedUser.FirstName).ThenByDescending(p => p.CreatedUser.LastName)
                            : query.OrderBy(p => p.CreatedUser.FirstName).ThenBy(p => p.CreatedUser.LastName);
                    }
                    break;
                case "statusProposal":
                    {
                        query = request.Order == "desc" ? query.OrderByDescending(p => p.StatusProposal) : query.OrderBy(p => p.StatusProposal);
                    }
                    break;
                default:
                    break;
            }
        }

        proposals = await query.ToListAsync(cancellationToken: cancellationToken);

        var smallProposals = _mapper.Map<List<SmallProposalResult>>(proposals);
        return (count, smallProposals);
    }
   
}