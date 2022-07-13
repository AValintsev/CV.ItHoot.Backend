using System.Collections.Generic;
using CVBuilder.Application.Resume.Services.Pagination;

namespace CVBuilder.Web.Contracts.V1.Requests.Proposal
{
    public class GetAllProposalsRequest : PaginationFilter
    {
        public string Term { get; set; }
        public List<int> Clients { get; set; }
        public List<int> Statuses { get; set; }

        public GetAllProposalsRequest() : base()
        { }

        public GetAllProposalsRequest(int? page, int? pageSize, string term, List<int> clients, List<int> statuses) : base(page, pageSize)
        {
            this.Term = term;
            this.Clients = clients;
            this.Statuses = statuses;
        }
    }
}
