using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Clients.ContentProcessor;

/// <summary>
/// Represents a request to summarize a paper.
/// </summary>
/// <param name="Title">The title of the paper.</param>
/// <param name="PageSummaries">The list of page summaries.</param>
public record SummarizePaperRequest(string Title, List<PageSummary> PageSummaries);
