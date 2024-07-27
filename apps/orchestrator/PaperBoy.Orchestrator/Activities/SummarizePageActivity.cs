using Dapr.Workflow;
using PaperBoy.Orchestrator.Clients.ContentProcessor;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Represents an activity to summarize a specific page of a paper.
/// </summary>
/// <param name="contentProcessorClient">The client to process the content.</param>
public class SummarizePageActivity(IContentProcessorClient contentProcessorClient): WorkflowActivity<SummarizePageActivityInput, SummarizePageActivityOutput>
{
    /// <summary>
    /// Asynchronously runs the activity to summarize a page.
    /// </summary>
    /// <param name="context">The workflow activity context.</param>
    /// <param name="input">The input containing the page number, paper title, and page content.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the output with the page summary.</returns>
    public override async Task<SummarizePageActivityOutput> RunAsync(WorkflowActivityContext context, SummarizePageActivityInput input)
    {
        var response = await contentProcessorClient.SummarizePageAsync(new SummarizePageRequest(
                input.PageNumber, 
                input.PaperTitle,
                input.PageContent));

        return new SummarizePageActivityOutput(response.Summary);
    }
}