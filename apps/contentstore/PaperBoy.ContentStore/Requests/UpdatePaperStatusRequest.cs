using PaperBoy.ContentStore.Domain;

namespace PaperBoy.ContentStore.Requests;

/// <summary>
/// Represents a request to update the status of a paper.
/// </summary>
public class UpdatePaperStatusRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the paper.
    /// </summary>
    public Guid PaperId { get; set; }

    /// <summary>
    /// Gets or sets the status of the paper.
    /// </summary>
    public PaperStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the summary of the paper.
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Gets or sets the score of the paper.
    /// </summary>
    public int? Score { get; set; }

    /// <summary>
    /// Gets or sets the explanation for the score.
    /// </summary>
    public string? ScoreExplanation { get; set; }

    /// <summary>
    /// Gets or sets the description of the paper.
    /// </summary>
    public string? Description { get; set; }
}