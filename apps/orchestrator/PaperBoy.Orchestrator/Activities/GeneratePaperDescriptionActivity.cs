using Dapr.Workflow;
using PaperBoy.Orchestrator.Clients.ContentProcessor;
using PaperBoy.Orchestrator.Clients.ContentStore;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Generates a newsletter description for a summarized paper.
/// </summary>
/// <param name="contentProcessorClient">Content processor client to use.</param>
public class GeneratePaperDescriptionActivity(IContentProcessorClient contentProcessorClient): WorkflowActivity<GeneratePaperDescriptionActivityInput, GeneratePaperDescriptionActivityOutput>
{
    /// <summary>
    /// Executes the workflow activity.
    /// </summary>
    /// <param name="context">Context information for the activity.</param>
    /// <param name="input">Input for the workflow activity.</param>
    /// <returns>Returns the output of the workflow activity.</returns>
    public override async Task<GeneratePaperDescriptionActivityOutput> RunAsync(WorkflowActivityContext context, GeneratePaperDescriptionActivityInput input)
    {
        var request = new GeneratePaperDescriptionRequest(input.Title, input.Summary);
        var response = await contentProcessorClient.GeneratePaperDescriptionAsync(request);

        return new GeneratePaperDescriptionActivityOutput(response.Description);
    }
}