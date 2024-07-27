namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Represents the input for the SubmitPageSummaryActivity.
/// </summary>
/// <param name="PaperId">The unique identifier of the paper.</param>
/// <param name="PageNumber">The number of the page being summarized.</param>
/// <param name="Summary">The summary of the page.</param>
public record SubmitPageSummaryActivityInput(Guid PaperId, int PageNumber, string Summary);