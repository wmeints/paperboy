using Dapr.Workflow;
using PaperBoy.Orchestrator.Clients;
using PaperBoy.Orchestrator.Clients.ContentStore;

namespace PaperBoy.Orchestrator.Activities;

public class SubmitPaperSummaryActivity(IContentStoreClient contentStoreClient): WorkflowActivity<SubmitPaperSummaryActivityInput, object>
{
    public override async Task<object> RunAsync(WorkflowActivityContext context, SubmitPaperSummaryActivityInput input)
    {
        await contentStoreClient.SubmitPaperSummary(input.PaperId, new SubmitPaperSummaryRequest(input.Summary, input.PageSummaries));
        return new object();
    }
}