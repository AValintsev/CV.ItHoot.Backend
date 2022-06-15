using CVBuilder.Web.Contracts.V1.Responses.Pagination;

namespace CVBuilder.Web.Contracts.V1.Requests.Resume
{
    public class GetAllResumeCardRequest : PaginationFilter
    {
        public string Term { get; set; }

        public GetAllResumeCardRequest() : base()
        {}

        public GetAllResumeCardRequest(string term, int page, int pageSize) : base(page, pageSize)
        {
            this.Term = term;
        }
    }
}
