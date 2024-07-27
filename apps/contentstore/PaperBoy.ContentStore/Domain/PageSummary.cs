namespace PaperBoy.ContentStore.Domain;

/// <summary>
/// Represents a summary of a specific page.
/// </summary>
/// <param name="PageNumber">The number of the page.</param>
/// <param name="Summary">The summary of the page.</param>
public record PageSummary(int PageNumber, string Summary);