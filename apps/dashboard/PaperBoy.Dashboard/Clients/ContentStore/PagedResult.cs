namespace PaperBoy.Dashboard.Clients.ContentStore;

public record PagedResult<T>(IEnumerable<T> Items, int Page, int PageSize, int TotalCount)
{
    public bool HasPreviousPage => Page > 0;
    public bool HasNextPage => (Page + 1) * PageSize < TotalCount;
}