namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Input for the DeclinePaperActivity.
/// </summary>
/// <param name="PaperId">The unique identifier of the paper to be declined.</param>
public record DeclinePaperActivityInput(Guid PaperId);