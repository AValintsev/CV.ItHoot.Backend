namespace CVBuilder.Web.Contracts.V1.Responses.Pagination;

public class PaginationFilter
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string Sort { get; set; }
    public string Order { get; set; }

    public PaginationFilter()
    {
        Page = 1;
        PageSize = 30;
    }
    public PaginationFilter(int page, int pageSize)
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize > 30 ? 30 : pageSize;
    }
}