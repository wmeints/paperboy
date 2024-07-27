namespace PaperBoy.ContentStore.Domain.Commands;

/// <summary>
/// Represents a command to submit a score for a paper.
/// </summary>
/// <param name="PaperId">The unique identifier of the paper.</param>
/// <param name="Score">The score assigned to the paper.</param>
/// <param name="Explanation">The explanation for the given score.</param>
public record SubmitScoreCommand(Guid PaperId, int Score, string Explanation);