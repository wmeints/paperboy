namespace PaperBoy.ContentStore.Domain.Events;

/// <summary>
/// Represents an event that occurs when a score is generated for a paper.
/// </summary>
/// <param name="PaperId">The unique identifier of the paper.</param>
/// <param name="Score">The score assigned to the paper.</param>
/// <param name="Explanation">The explanation for the given score.</param>
public record ScoreGeneratedEvent(Guid PaperId, int Score, string Explanation);
