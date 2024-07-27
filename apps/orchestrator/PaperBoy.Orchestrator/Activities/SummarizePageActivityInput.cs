namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Represents the input for the SummarizePageActivity.
/// </summary>
/// <param name="PageNumber">The number of the page to be summarized.</param>
/// <param name="PaperTitle">The title of the paper.</param>
/// <param name="PageContent">The content of the page to be summarized.</param>
public record SummarizePageActivityInput(int PageNumber, string PaperTitle, string PageContent);