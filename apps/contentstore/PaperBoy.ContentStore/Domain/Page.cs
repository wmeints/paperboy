namespace PaperBoy.ContentStore.Domain;

/// <summary>
/// Represents a page with its number, content, and an optional summary.
/// </summary>
/// <param name="PageNumber">The number of the page.</param>
/// <param name="Content">The content of the page.</param>
/// <param name="Summary">The optional summary of the page.</param>
public record Page(int PageNumber, string Content, string? Summary = null);
