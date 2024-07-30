using Dapr.Workflow;
using PaperBoy.Orchestrator.Clients.ContentStore;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Activity for declining a paper.
/// </summary>
/// <param name="contentStoreClient">The client for interacting with the content store.</param>
public class DeclinePaperActivity(IContentStoreClient contentStoreClient): WorkflowActivity<DeclinePaperActivityInput, object>
{
    /// <summary>
    /// Runs the activity to decline a paper.
    /// </summary>
    /// <param name="context">The workflow activity context.</param>
    /// <param name="input">The input containing the paper ID.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is an object.</returns>
    public override async Task<object> RunAsync(WorkflowActivityContext context, DeclinePaperActivityInput input)
    {
        await contentStoreClient.DeclinePaperAsync(input.PaperId);
        return new object();
    }
}