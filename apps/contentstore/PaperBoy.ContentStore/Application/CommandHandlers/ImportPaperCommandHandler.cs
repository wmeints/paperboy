using PaperBoy.ContentStore.Domain;
using PaperBoy.ContentStore.Domain.Commands;

namespace PaperBoy.ContentStore.Application.CommandHandlers;

/// <summary>
/// Imports a paper in the system.
/// </summary>
/// <param name="paperRepository">Repository for the paper data.</param>
/// <param name="contentExtractor">Content extractor to use for importing the data.</param>
public class ImportPaperCommandHandler(IPaperRepository paperRepository, IContentExtractor contentExtractor)
{
    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="cmd">Data to use for executing the command.</param>
    public async Task ExecuteAsync(ImportPaperCommand cmd)
    {
        var paper = await Paper.ImportAsync(cmd, contentExtractor);
        await paperRepository.SaveAsync(paper);
    }
}