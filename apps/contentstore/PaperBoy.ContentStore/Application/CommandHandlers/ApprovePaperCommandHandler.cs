using PaperBoy.ContentStore.Domain;
using PaperBoy.ContentStore.Domain.Commands;

namespace PaperBoy.ContentStore.Application.CommandHandlers;

/// <summary>
/// Handler for approving a paper.
/// </summary>
/// <param name="paperRepository">The repository for accessing paper data.</param>
public class ApprovePaperCommandHandler(IPaperRepository paperRepository)
{
    /// <summary>
    /// Executes the command to approve a paper.
    /// </summary>
    /// <param name="command">The command containing the paper ID.</param>
    /// <exception cref="PaperNotFoundException">Thrown when the paper with the specified ID is not found.</exception>
    public async Task ExecuteAsync(ApprovePaperCommand command)
    {
        var paper = await paperRepository.GetByIdAsync(command.PaperId);

        if (paper == null)
        {
            throw new PaperNotFoundException($"Paper with id {command.PaperId} not found.");
        }

        paper.Approve(command);

        await paperRepository.SaveAsync(paper);
    }
}
