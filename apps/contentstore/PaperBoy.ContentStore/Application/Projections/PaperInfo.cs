using PaperBoy.ContentStore.Domain;

namespace PaperBoy.ContentStore.Application.Projections;

/// <summary>
/// Represents a read model for easier querying of paper information.
/// </summary>
public class PaperInfo
{
    /// <summary>
    /// Gets or sets the unique identifier for the paper.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the paper.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Gets or sets the number of sections that have been summarized.
    /// </summary>
    public int SectionsSummarized { get; set; }

    /// <summary>
    /// Gets or sets the total number of sections in the paper.
    /// </summary>
    public int TotalSections { get; set; }

    /// <summary>
    /// Gets or sets the summary of the paper.
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Gets or sets the score of the paper.
    /// </summary>
    public PaperScore? Score { get; set; }

    /// <summary>
    /// Gets or sets the status of the paper.
    /// </summary>
    public PaperStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the date the paper was created.
    /// </summary>
    public DateTime DateCreated { get; set; }
}