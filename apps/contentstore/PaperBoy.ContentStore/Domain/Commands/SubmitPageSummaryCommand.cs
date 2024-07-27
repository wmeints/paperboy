namespace PaperBoy.ContentStore.Domain.Commands;

/// <summary>
/// Represents a command to submit a summary for a specific page of a paper.
/// </summary>
/// <param name="PaperId">The unique identifier of the paper.</param>
/// <param name="PageNumber">The number of the page being summarized.</param>
/// <param name="Summary">The summary of the page.</param>
public record SubmitPageSummaryCommand(Guid PaperId, int PageNumber, string Summary);