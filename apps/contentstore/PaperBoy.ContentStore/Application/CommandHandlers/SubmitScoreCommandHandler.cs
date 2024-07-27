using PaperBoy.ContentStore.Domain;
using PaperBoy.ContentStore.Domain.Commands;

namespace PaperBoy.ContentStore.Application.CommandHandlers;

/// <summary>
/// Submits a generated score for a paper.
/// </summary>
/// <param name="paperRepository">Repository for the paper data.</param>
public class SubmitScoreCommandHandler(IPaperRepository paperRepository)
{
    /// <summary>
    /// Executes a command to submit a score for a paper
    /// </summary>
    /// <param name="cmd">Command data to use for executing the command.</param>
    /// <exception cref="PaperNotFoundException">Gets thrown when the paper was not found.</exception>
    public async Task ExecuteAsync(SubmitScoreCommand cmd)
    {
        var paper = await paperRepository.GetByIdAsync(cmd.PaperId);

        if (paper == null)
        {
            throw new PaperNotFoundException($"Paper '{cmd.PaperId}' not found");
        }
        
        paper.SubmitScore(cmd);

        await paperRepository.SaveAsync(paper);
    }
}