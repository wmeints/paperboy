using Dapr.Workflow;
using PaperBoy.Orchestrator.Clients.ContentProcessor;
using PaperBoy.Orchestrator.Clients.ContentStore;
using ContentProcessor = PaperBoy.Orchestrator.Clients.ContentProcessor;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Generates a new score for a paper with an explanation for the score.
/// </summary>
public class ScorePaperActivity(IContentProcessorClient contentProcessorClient, IContentStoreClient contentStoreClient): WorkflowActivity<ScorePaperActivityInput, ScorePaperActivityOutput>
{
    public override async Task<ScorePaperActivityOutput> RunAsync(WorkflowActivityContext context, ScorePaperActivityInput input)
    {
        var request = new GeneratePaperScoreRequest(input.Title, input.Summary);
        var response = await contentProcessorClient.GeneratePaperScoreAsync(request);

        return new ScorePaperActivityOutput(response.Score, response.Explanation);
    }
}