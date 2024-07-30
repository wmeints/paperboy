using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Input for the <see cref="SummarizePaperActivity"/>.
/// </summary>
/// <param name="PaperTitle">Title of the paper.</param>
/// <param name="PageSummaries">Summaries of all pages in the paper.</param>
public record SummarizePaperActivityInput(string PaperTitle, List<PageSummary> PageSummaries);