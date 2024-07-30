using Dapr.Workflow;
using PaperBoy.Orchestrator.Clients.ContentProcessor;
using PaperBoy.Orchestrator.Clients.ContentStore;
using PaperBoy.Orchestrator.Models;
using PageData = PaperBoy.Orchestrator.Models.PageData;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Represents an activity to summarize an entire paper.
/// </summary>
/// <param name="contentStoreClient">The client to access the content store.</param>
/// <param name="contentProcessorClient">The client to process the content.</param>
public class SummarizePaperActivity(IContentStoreClient contentStoreClient, IContentProcessorClient contentProcessorClient): WorkflowActivity<SummarizePaperActivityInput, SummarizePaperActivityOutput>
{
    /// <summary>
    /// Asynchronously runs the activity to summarize a paper.
    /// </summary>
    /// <param name="context">The workflow activity context.</param>
    /// <param name="input">The input containing the paper ID.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the output with the paper summary and page summaries.</returns>
    public override async Task<SummarizePaperActivityOutput> RunAsync(WorkflowActivityContext context, SummarizePaperActivityInput input)
    {
        var request = new SummarizePaperRequest(input.PaperTitle, input.PageSummaries);
        var response = await contentProcessorClient.SummarizePaperAsync(request);

        return new SummarizePaperActivityOutput(response.Summary, response.PageSummaries);
    }
}