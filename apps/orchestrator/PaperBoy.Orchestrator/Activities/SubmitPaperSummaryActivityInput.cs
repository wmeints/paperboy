using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Input for the <see cref="SubmitPaperSummaryActivity"/>
/// </summary>
/// <param name="PaperId">Identifier for the paper.</param>
/// <param name="Summary">Summary for the paper.</param>
public record SubmitPaperSummaryActivityInput(Guid PaperId, string Summary);