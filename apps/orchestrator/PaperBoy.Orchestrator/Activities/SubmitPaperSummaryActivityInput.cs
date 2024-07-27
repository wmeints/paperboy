using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Activities;

public record SubmitPaperSummaryActivityInput(Guid PaperId, string Summary, List<PageSummary> PageSummaries);