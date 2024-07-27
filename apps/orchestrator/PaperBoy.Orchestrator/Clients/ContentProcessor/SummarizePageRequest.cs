namespace PaperBoy.Orchestrator.Clients.ContentProcessor;

/// <summary>
/// Represents a request to summarize a specific page of a paper.
/// </summary>
/// <param name="PageNumber">The number of the page to be summarized.</param>
/// <param name="PaperTitle">The title of the paper.</param>
/// <param name="PageContent">The content of the page to be summarized.</param>
public record SummarizePageRequest(int PageNumber, string PaperTitle, string PageContent);