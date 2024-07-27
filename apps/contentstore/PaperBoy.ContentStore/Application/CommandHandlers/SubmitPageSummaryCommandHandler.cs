using PaperBoy.ContentStore.Domain;
using PaperBoy.ContentStore.Domain.Commands;

namespace PaperBoy.ContentStore.Application.CommandHandlers;

/// <summary>
/// Handles the command to submit a page summary.
/// </summary>
/// <param name="paperRepository">The repository to access paper data.</param>
public class SubmitPageSummaryCommandHandler(IPaperRepository paperRepository)
{
    /// <summary>
    /// Asynchronously executes the command to submit a page summary.
    /// </summary>
    /// <param name="command">The command containing the page summary details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the paper with the specified ID is not found.</exception>
    public async Task ExecuteAsync(SubmitPageSummaryCommand command)
    {
        var paper = await paperRepository.GetByIdAsync(command.PaperId);

        if (paper == null)
        {
            throw new InvalidOperationException($"Paper with id {command.PaperId} not found");
        }

        paper.SubmitPageSummary(command);

        await paperRepository.SaveAsync(paper);
    }
}