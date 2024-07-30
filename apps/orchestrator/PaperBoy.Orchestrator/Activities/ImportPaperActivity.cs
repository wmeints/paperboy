using Dapr.Client;
using Dapr.Workflow;
using PaperBoy.Orchestrator.Clients;
using PaperBoy.Orchestrator.Clients.ContentStore;
using PaperBoy.Orchestrator.Workflows;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Imports a paper from a given URL into the content store.
/// </summary>
/// <param name="contentStoreClient">Content store client to use for communicating with the content store.</param>
public class ImportPaperActivity(IContentStoreClient contentStoreClient): WorkflowActivity<ImportPaperActivityInput, ImportPaperActivityOutput>
{
    /// <summary>
    /// Executes the import paper activity asynchronously.
    /// </summary>
    /// <param name="context">The workflow activity context.</param>
    /// <param name="input">The input for the import paper activity.</param>
    /// <returns>The output of the import paper activity.</returns>
    public override async Task<ImportPaperActivityOutput> RunAsync(WorkflowActivityContext context, ImportPaperActivityInput input)
    {
        var result = await contentStoreClient.ImportPaperAsync(new ImportPaperRequest(input.Title, input.Url, input.Submitter));
        return new ImportPaperActivityOutput(result.PaperId);
    }
}