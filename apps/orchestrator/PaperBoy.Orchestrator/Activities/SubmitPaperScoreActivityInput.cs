namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Input for the <see cref="SubmitPaperScoreActivity"/>.
/// </summary>
/// <param name="PaperId">ID for the paper.</param>
/// <param name="Score">Generated score.</param>
/// <param name="Explanation">Generated explanation for the score.</param>
public record SubmitPaperScoreActivityInput(Guid PaperId, int Score, string Explanation);