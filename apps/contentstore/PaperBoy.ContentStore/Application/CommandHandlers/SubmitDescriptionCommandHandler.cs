using PaperBoy.ContentStore.Domain;
using PaperBoy.ContentStore.Domain.Commands;

namespace PaperBoy.ContentStore.Application.CommandHandlers;

/// <summary>
/// Handler for submitting a description for a paper.
/// </summary>
/// <param name="paperRepository">The repository for accessing paper data.</param>
public class SubmitDescriptionCommandHandler(IPaperRepository paperRepository)
{
    /// <summary>
    /// Executes the command to submit a description for a paper.
    /// </summary>
    /// <param name="cmd">The command containing the paper ID and description.</param>
    /// <exception cref="PaperNotFoundException">Thrown when the paper with the specified ID is not found.</exception>
    public async Task ExecuteAsync(SubmitDescriptionCommand cmd)
    {
        var paper = await paperRepository.GetByIdAsync(cmd.PaperId);

        if (paper == null)
        {
            throw new PaperNotFoundException($"Paper with id {cmd.PaperId} not found");
        }

        paper.SubmitDescription(cmd);

        await paperRepository.SaveAsync(paper);
    }
}