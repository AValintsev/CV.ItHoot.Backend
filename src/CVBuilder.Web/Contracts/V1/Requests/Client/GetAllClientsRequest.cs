using CVBuilder.Application.Resume.Services.Pagination;

namespace CVBuilder.Web.Contracts.V1.Requests.Client
{
    public class GetAllClientsRequest : PaginationFilter
    {
        public string Term { get; set; }

        public GetAllClientsRequest() : base()
        { }

        public GetAllClientsRequest(int? page, int? pageSize, string term) : base(page, pageSize)
        {
            this.Term = term;
        }
    }
}
