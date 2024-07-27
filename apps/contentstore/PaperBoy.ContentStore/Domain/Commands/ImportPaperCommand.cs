namespace PaperBoy.ContentStore.Domain.Commands;

/// <summary>
/// Command to import a paper from a URL
/// </summary>
/// <param name="PaperId">Generated Paper ID.</param>
/// <param name="Title">Title of the paper.</param>
/// <param name="Url">URL for the paper to download from.</param>
/// <param name="Submitter">The information about the submitter of the paper.</param>
public record ImportPaperCommand(Guid PaperId, string Title, string Url, SubmitterInformation Submitter);
