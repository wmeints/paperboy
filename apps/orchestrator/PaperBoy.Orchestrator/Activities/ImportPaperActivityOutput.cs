namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// The output of the <see cref="ImportPaperActivity"/>.
/// </summary>
/// <param name="PaperId">Identifier for the imported paper.</param>
public record ImportPaperActivityOutput(Guid PaperId);