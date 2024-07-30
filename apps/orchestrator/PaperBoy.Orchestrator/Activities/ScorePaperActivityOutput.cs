namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Output of the <see cref="ScorePaperActivity"/>
/// </summary>
/// <param name="Score">Score received for the paper.</param>
/// <param name="Explanation">Explanation of the score.</param>
public record ScorePaperActivityOutput(int Score, string Explanation);