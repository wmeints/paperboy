namespace PaperBoy.ContentStore.Domain.Events;

/// <summary>
/// Represents an event that is triggered when a page summary is generated.
/// </summary>
/// <param name="PaperId">The unique identifier of the paper.</param>
/// <param name="PageNumber">The number of the page that was summarized.</param>
/// <param name="Summary">The summary of the page.</param>
public record PageSummaryGeneratedEvent(Guid PaperId, int PageNumber, string Summary);