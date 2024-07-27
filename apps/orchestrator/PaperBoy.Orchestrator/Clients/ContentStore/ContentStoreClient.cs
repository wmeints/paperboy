using Dapr.Client;

namespace PaperBoy.Orchestrator.Clients.ContentStore;

public class ContentStoreClient(DaprClient daprClient) : IContentStoreClient
{
    public async Task<ImportPaperResult> ImportPaperAsync(ImportPaperRequest request)
    {
        var result = await daprClient.InvokeMethodAsync<ImportPaperRequest, ImportPaperResult>(
            HttpMethod.Post, "contentstore", "import", request);

        return result;
    }

    public async Task SubmitPaperSummary(Guid paperId, SubmitPaperSummaryRequest request)
    {
        await daprClient.InvokeMethodAsync(HttpMethod.Put, "contentstore", $"papers/{paperId}/summary", request);
    }

    public async Task SubmitPaperScoreAsync(Guid paperId, SubmitPaperScoreRequest request)
    {
        await daprClient.InvokeMethodAsync(HttpMethod.Put, "contentstore", $"papers/{paperId}/score", request);
    }

    public async Task<GetPaperResponse> GetPaperAsync(Guid paperId)
    {
        return await daprClient.InvokeMethodAsync<GetPaperResponse>(HttpMethod.Get, "contentstore", $"papers/{paperId}");
    }
}