using CVBuilder.Web.Contracts.V1.Responses.Pagination;
using System.Collections.Generic;

namespace CVBuilder.Web.Contracts.V1.Requests.Resume
{
    public class GetAllResumeCardRequest : PaginationFilter
    {
        public string Term { get; set; }

        public List<int> Positions { get; set; }

        public List<int> Skills { get; set; }

        public GetAllResumeCardRequest() : base()
        {}

        public GetAllResumeCardRequest(int page, int pageSize, string term, List<int> positions, List<int> skills) : base(page, pageSize)
        {
            this.Term = term;
            this.Positions = positions;
            this.Skills = skills;
        }
    }
}
