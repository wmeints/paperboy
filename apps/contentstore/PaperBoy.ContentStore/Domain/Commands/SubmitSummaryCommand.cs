namespace PaperBoy.ContentStore.Domain.Commands;

/// <summary>
/// Represents a command to submit a summary for a paper.
/// </summary>
/// <param name="PaperId">The unique identifier of the paper.</param>
/// <param name="Summary">The summary of the paper.</param>
/// <param name="PageSummaries">The list of page summaries.</param>
public record SubmitSummaryCommand(Guid PaperId, string Summary, List<PageSummary> PageSummaries);