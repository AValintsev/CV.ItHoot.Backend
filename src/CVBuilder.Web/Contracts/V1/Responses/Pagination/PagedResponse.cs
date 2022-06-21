namespace CVBuilder.Web.Contracts.V1.Responses.Pagination;

public class PagedResponse<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public T Items { get; set; }
    public PagedResponse(T items, int page, int pageSize, int totalRecords)
    {
        this.Page = page;
        this.PageSize = pageSize;
        this.TotalRecords = totalRecords;
        this.Items = items;
    }
}