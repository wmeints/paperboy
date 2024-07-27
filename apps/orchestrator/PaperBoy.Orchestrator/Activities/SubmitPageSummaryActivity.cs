using Dapr.Workflow;
using PaperBoy.Orchestrator.Clients.ContentStore;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Represents an activity to submit a summary for a specific page of a paper.
/// </summary>
/// <param name="contentStoreClient">The client to access the content store.</param>
public class SubmitPageSummaryActivity(IContentStoreClient contentStoreClient): WorkflowActivity<SubmitPageSummaryActivityInput, object>
{
    /// <summary>
    /// Asynchronously runs the activity to submit a page summary.
    /// </summary>
    /// <param name="context">The workflow activity context.</param>
    /// <param name="input">The input containing the paper ID, page number, and summary.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is an object.</returns>
    public override async Task<object> RunAsync(WorkflowActivityContext context, SubmitPageSummaryActivityInput input)
    {
        await contentStoreClient.SubmitPageSummary(
            input.PaperId,
            input.PageNumber, 
            new SubmitPageSummaryRequest(input.Summary));

        return new object();
    }
}