namespace PaperBoy.ContentStore.Responses;

/// <summary>
/// Represents the response for importing a paper.
/// </summary>
/// <param name="PaperId">The unique identifier of the imported paper.</param>
public record ImportPaperResponse(Guid PaperId);