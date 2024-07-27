namespace PaperBoy.ContentStore.Domain.Events;

/// <summary>
/// Represents an event that occurs when a summary is generated for a paper.
/// </summary>
/// <param name="PaperId">The unique identifier of the paper.</param>
/// <param name="Summary">The summary of the paper.</param>
/// <param name="PageSummaries">The list of page summaries.</param>
public record SummaryGeneratedEvent(Guid PaperId, string Summary, List<PageSummary> PageSummaries);