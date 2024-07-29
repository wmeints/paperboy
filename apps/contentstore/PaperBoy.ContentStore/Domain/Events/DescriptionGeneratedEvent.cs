namespace PaperBoy.ContentStore.Domain.Events;

/// <summary>
/// Event that is triggered when a description is generated for a paper.
/// </summary>
/// <param name="PaperId">The unique identifier of the paper.</param>
/// <param name="Description">The generated description of the paper.</param>
public record DescriptionGeneratedEvent(Guid PaperId, string Description);