using PaperBoy.ContentStore.Domain;
using PaperBoy.ContentStore.Domain.Commands;

namespace PaperBoy.ContentStore.Application.CommandHandlers;

/// <summary>
/// Submits a new summary for a paper.
/// </summary>
/// <param name="paperRepository">Repository for the paper data.</param>
public class SubmitSummaryCommandHandler(IPaperRepository paperRepository)
{
    /// <summary>
    /// Executes the command to submit a summary for a paper.
    /// </summary>
    /// <param name="cmd">Command data to use for executing the command.</param>
    /// <exception cref="PaperNotFoundException">Gets thrown when the paper was not found.</exception>
    public async Task ExecuteAsync(SubmitSummaryCommand cmd)
    {
        var paper = await paperRepository.GetByIdAsync(cmd.PaperId);

        if (paper == null)
        {
            throw new PaperNotFoundException($"Paper '{cmd.PaperId} not found.");
        }

        paper.SubmitSummary(cmd);

        await paperRepository.SaveAsync(paper);
    }
}