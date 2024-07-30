using Dapr.Workflow;
using PaperBoy.Orchestrator.Clients;
using PaperBoy.Orchestrator.Clients.ContentStore;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Submits the generated score for the paper to the content store.
/// </summary>
/// <param name="contentStoreClient">Content store client to use.</param>
public class SubmitPaperScoreActivity(IContentStoreClient contentStoreClient): WorkflowActivity<SubmitPaperScoreActivityInput, object>
{
    /// <summary>
    /// Executes the workflow activity.
    /// </summary>
    /// <param name="context">Context of the activity.</param>
    /// <param name="input">Input for the activity.</param>
    /// <returns>Returns the output of the activity.</returns>
    public override async Task<object> RunAsync(WorkflowActivityContext context, SubmitPaperScoreActivityInput input)
    {
        await contentStoreClient.SubmitPaperScoreAsync(input.PaperId,
            new SubmitPaperScoreRequest(input.Score, input.Explanation));
        
        return new object();
    }
}