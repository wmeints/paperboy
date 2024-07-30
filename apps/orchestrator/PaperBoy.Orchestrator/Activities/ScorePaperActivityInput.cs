namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Input information for the <see cref="ScorePaperActivityInput"/>
/// </summary>
/// <param name="Title">Title of the paper.</param>
/// <param name="Summary">Summary for the paper.</param>
public record ScorePaperActivityInput(string Title, string Summary);