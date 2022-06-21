namespace CVBuilder.Web.Contracts.V1.Responses.Pagination;

public class PaginationFilter
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string Sort { get; set; }
    public string Order { get; set; }

    public PaginationFilter()
    {
        this.Page = 1;
        this.PageSize = 30;
    }
    public PaginationFilter(int page, int pageSize)
    {
        this.Page = page < 1 ? 1 : page;
        this.PageSize = pageSize > 30 ? 30 : pageSize;
    }
}