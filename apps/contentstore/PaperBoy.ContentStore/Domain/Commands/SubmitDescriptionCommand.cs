namespace PaperBoy.ContentStore.Domain.Commands;

/// <summary>
/// Command to submit a description for a paper.
/// </summary>
/// <param name="PaperId">The unique identifier of the paper.</param>
/// <param name="Description">The description of the paper.</param>
public record SubmitDescriptionCommand(Guid PaperId, string Description);