using PaperBoy.ContentStore.Application.Projections;

namespace PaperBoy.ContentStore.Responses;

/// <summary>
/// Represents the response for retrieving a list of papers with pagination.
/// </summary>
/// <param name="Items">The list of papers.</param>
/// <param name="Page">The current page number.</param>
/// <param name="PageSize">The number of items per page.</param>
/// <param name="TotalCount">The total number of items.</param>
public record GetPapersResponse(IEnumerable<PaperInfo> Items, int Page, int PageSize, int TotalCount);
