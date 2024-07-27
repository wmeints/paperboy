namespace PaperBoy.ContentStore.Domain.Events;

/// <summary>
/// Represents an event that occurs when a paper is imported.
/// </summary>
/// <param name="PaperId">The unique identifier of the paper.</param>
/// <param name="Url">The URL from which the paper was imported.</param>
/// <param name="Title">The title of the paper.</param>
/// <param name="Submitter">Information about the submitter of the paper.</param>
/// <param name="Pages">The list of pages in the paper.</param>
public record PaperImportedEvent(
    Guid PaperId,
    string Url,
    string Title,
    SubmitterInformation Submitter,
    List<Page> Pages);