namespace PaperBoy.Orchestrator.Clients.ContentProcessor;

/// <summary>
/// Represents the response for summarizing a specific page of a paper.
/// </summary>
/// <param name="Summary">The summary of the page.</param>
public record SummarizePageResponse(string Summary);