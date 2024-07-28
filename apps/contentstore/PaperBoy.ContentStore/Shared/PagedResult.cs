namespace PaperBoy.ContentStore.Application.Projections;

public record PagedResult<T>(IEnumerable<T> Items, int Page, int PageSize, int TotalCount);