namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Represents the input for the GetPaperDetailsActivity.
/// </summary>
/// <param name="PaperId">The unique identifier of the paper.</param>
public record GetPaperDetailsActivityInput(Guid PaperId);