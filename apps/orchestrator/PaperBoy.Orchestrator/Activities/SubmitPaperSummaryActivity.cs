using Dapr.Workflow;
using PaperBoy.Orchestrator.Clients;
using PaperBoy.Orchestrator.Clients.ContentStore;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Submits the summary for the paper to the content store.
/// </summary>
/// <param name="contentStoreClient">Content store client instance to use.</param>
public class SubmitPaperSummaryActivity(IContentStoreClient contentStoreClient): WorkflowActivity<SubmitPaperSummaryActivityInput, object>
{
    /// <summary>
    /// Executes the workflow activity.
    /// </summary>
    /// <param name="context">Context of the activity.</param>
    /// <param name="input">Input for the activity.</param>
    /// <returns>Returns the output of the activity.</returns>
    public override async Task<object> RunAsync(WorkflowActivityContext context, SubmitPaperSummaryActivityInput input)
    {
        await contentStoreClient.SubmitPaperSummary(input.PaperId, new SubmitPaperSummaryRequest(input.Summary));
        return new object();
    }
}