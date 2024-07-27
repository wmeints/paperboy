namespace PaperBoy.ContentStore.Responses;

/// <summary>
/// Represents the data for a page in a paper.
/// </summary>
/// <param name="PageNumber">The number of the page.</param>
/// <param name="Content">The content of the page.</param>
/// <param name="Summary">The summary of the page, if available.</param>
public record PageData(int PageNumber, string Content, string? Summary);