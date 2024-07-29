using Dapr.Workflow;
using PaperBoy.Orchestrator.Clients.ContentStore;

namespace PaperBoy.Orchestrator.Activities;

public record SubmitPaperDescriptionActivityInput(Guid PaperId, string PaperDescription);

/// <summary>
/// Submits the generated newsletter description for the paper to the content store.
/// </summary>
/// <param name="contentStoreClient">Content store client to communicate with the content store.</param>
public class SubmitPaperDescriptionActivity(IContentStoreClient contentStoreClient): WorkflowActivity<SubmitPaperDescriptionActivityInput, object>
{
    /// <summary>
    /// Executes the workflow activity.
    /// </summary>
    /// <param name="context">Context of the activity.</param>
    /// <param name="input">Input for the activity.</param>
    /// <returns>Returns the output of the activity.</returns>
    public override async Task<object> RunAsync(WorkflowActivityContext context, SubmitPaperDescriptionActivityInput input)
    {
        await contentStoreClient.SubmitPaperDescriptionAsync(input.PaperId, 
            new SubmitPaperDescriptionRequest(input.PaperDescription));
        
        return new object();
    }
}