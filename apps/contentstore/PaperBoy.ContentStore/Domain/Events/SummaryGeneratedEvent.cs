namespace PaperBoy.ContentStore.Domain.Events;

/// <summary>
/// Represents an event that occurs when a summary is generated for a paper.
/// </summary>
/// <param name="PaperId">The unique identifier of the paper.</param>
/// <param name="Summary">The summary of the paper.</param>
public record SummaryGeneratedEvent(Guid PaperId, string Summary);