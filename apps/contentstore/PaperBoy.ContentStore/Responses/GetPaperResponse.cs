namespace PaperBoy.ContentStore.Responses;

/// <summary>
/// Represents the response for retrieving a paper.
/// </summary>
/// <param name="Id">The unique identifier of the paper.</param>
/// <param name="Title">The title of the paper.</param>
/// <param name="Summary">The summary of the paper.</param>
/// <param name="Pages">The list of pages in the paper.</param>
public record GetPaperResponse(Guid Id, string Title, string? Summary, List<PageData> Pages);