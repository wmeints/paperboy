using Marten;
using PaperBoy.ContentStore.Domain;

namespace PaperBoy.ContentStore.Infrastructure;

/// <summary>
/// Implementation of the <see cref="IPaperRepository"/> interface.
/// </summary>
/// <param name="documentStore"></param>
public class PaperRepository(IDocumentStore documentStore): IPaperRepository
{
    /// <summary>
    /// Retrieves a paper by its id.
    /// </summary>
    /// <param name="id">ID of the paper to retrieve.</param>
    /// <returns>Returns the found paper.</returns>
    public async Task<Paper?> GetByIdAsync(Guid id)
    {
        await using var session = await documentStore.LightweightSerializableSessionAsync();
        var paper = await session.Events.AggregateStreamAsync<Paper>(id);

        return paper;
    }

    /// <summary>
    /// Saves a paper in the database.
    /// </summary>
    /// <param name="paper">The paper to save.</param>
    public async Task SaveAsync(Paper paper)
    {
        var session = await documentStore.LightweightSerializableSessionAsync();
        session.Events.Append(paper.Id, paper.Version, paper.PendingDomainEvents);
        await session.SaveChangesAsync();
        
        paper.ClearPendingDomainEvents();
    }
}