using PaperBoy.ContentStore.Domain;

namespace PaperBoy.ContentStore.Requests;

/// <summary>
/// Represents a request to import a paper.
/// </summary>
public class ImportPaperRequest
{
    /// <summary>
    /// Gets or sets the ID of the paper.
    /// </summary>
    public Guid PaperId { get; set; }
    
    /// <summary>
    /// Gets or sets the title of the paper.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Gets or sets the URL of the paper.
    /// </summary>
    public string Url { get; set; } = default!;

    /// <summary>
    /// Gets or sets the information of the submitter.
    /// </summary>
    public SubmitterInformation Submitter { get; set; } = default!;
}