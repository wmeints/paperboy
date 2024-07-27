namespace PaperBoy.ContentStore.Domain;

/// <summary>
/// Defines a contract for a repository that manages papers.
/// </summary>
public interface IPaperRepository
{
    /// <summary>
    /// Asynchronously retrieves a paper by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the paper.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the paper if found; otherwise, null.</returns>
    Task<Paper?> GetByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously saves the specified paper.
    /// </summary>
    /// <param name="paper">The paper to save.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SaveAsync(Paper paper);
}