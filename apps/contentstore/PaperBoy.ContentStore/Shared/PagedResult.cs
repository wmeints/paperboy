namespace PaperBoy.ContentStore.Shared;

public record PagedResult<T>(IEnumerable<T> Items, int Page, int PageSize, int TotalCount);