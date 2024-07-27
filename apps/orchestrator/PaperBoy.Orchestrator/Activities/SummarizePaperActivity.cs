using Dapr.Workflow;
using PaperBoy.Orchestrator.Clients.ContentProcessor;
using PaperBoy.Orchestrator.Clients.ContentStore;
using PageData = PaperBoy.Orchestrator.Models.PageData;

namespace PaperBoy.Orchestrator.Activities;

public class SummarizePaperActivity(IContentStoreClient contentStoreClient, IContentProcessorClient contentProcessorClient): WorkflowActivity<SummarizePaperActivityInput, SummarizePaperActivityOutput>
{
    public override async Task<SummarizePaperActivityOutput> RunAsync(WorkflowActivityContext context, SummarizePaperActivityInput input)
    {
        var paper = await contentStoreClient.GetPaperAsync(input.PaperId);
        var request = new SummarizePaperRequest(paper.Title, paper.Pages.Select(x => new PageData(x.PageNumber, x.Content)).ToList());
        var response = await contentProcessorClient.SummarizePaperAsync(request);

        return new SummarizePaperActivityOutput(response.Summary, response.PageSummaries);
    }
}