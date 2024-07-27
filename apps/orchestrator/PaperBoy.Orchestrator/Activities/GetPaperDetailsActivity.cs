using Dapr.Workflow;
using PaperBoy.Orchestrator.Clients.ContentStore;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Represents an activity to get the details of a paper.
/// </summary>
/// <param name="contentStoreClient">The client to access the content store.</param>
public class GetPaperDetailsActivity(IContentStoreClient contentStoreClient) : WorkflowActivity<GetPaperDetailsActivityInput, GetPaperDetailsActivityOutput>
{
    /// <summary>
    /// Asynchronously runs the activity to get the details of a paper.
    /// </summary>
    /// <param name="context">The workflow activity context.</param>
    /// <param name="input">The input containing the paper ID.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the output with the paper title and pages.</returns>
    public override async Task<GetPaperDetailsActivityOutput> RunAsync(WorkflowActivityContext context, GetPaperDetailsActivityInput input)
    {
        var response = await contentStoreClient.GetPaperAsync(input.PaperId);
        return new GetPaperDetailsActivityOutput(response.Title, response.Pages);
    }
}