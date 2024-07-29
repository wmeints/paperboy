using Dapr.Client;

namespace PaperBoy.Orchestrator.Clients.ContentStore;

/// <summary>
/// Client for interacting with the content store using Dapr.
/// </summary>
/// <param name="daprClient">The Dapr client instance.</param>
public class ContentStoreClient(DaprClient daprClient) : IContentStoreClient
{
    /// <summary>
    /// Imports a paper asynchronously.
    /// </summary>
    /// <param name="request">The request containing the paper details to import.</param>
    /// <returns>The result of the import operation.</returns>
    public async Task<ImportPaperResult> ImportPaperAsync(ImportPaperRequest request)
    {
        var result = await daprClient.InvokeMethodAsync<ImportPaperRequest, ImportPaperResult>(
            HttpMethod.Post, "contentstore", "import", request);

        return result;
    }

    /// <summary>
    /// Submits a paper summary asynchronously.
    /// </summary>
    /// <param name="paperId">The ID of the paper.</param>
    /// <param name="request">The request containing the paper summary details.</param>
    public async Task SubmitPaperSummary(Guid paperId, SubmitPaperSummaryRequest request)
    {
        await daprClient.InvokeMethodAsync(HttpMethod.Put, "contentstore", $"papers/{paperId}/summary", request);
    }

    /// <summary>
    /// Submits a paper score asynchronously.
    /// </summary>
    /// <param name="paperId">The ID of the paper.</param>
    /// <param name="request">The request containing the paper score details.</param>
    public async Task SubmitPaperScoreAsync(Guid paperId, SubmitPaperScoreRequest request)
    {
        await daprClient.InvokeMethodAsync(HttpMethod.Put, "contentstore", $"papers/{paperId}/score", request);
    }

    /// <summary>
    /// Retrieves a paper asynchronously.
    /// </summary>
    /// <param name="paperId">The ID of the paper.</param>
    /// <returns>The response containing the paper details.</returns>
    public async Task<GetPaperResponse> GetPaperAsync(Guid paperId)
    {
        return await daprClient.InvokeMethodAsync<GetPaperResponse>(HttpMethod.Get, "contentstore", $"papers/{paperId}");
    }

    /// <summary>
    /// Submits a page summary asynchronously.
    /// </summary>
    /// <param name="paperId">The ID of the paper.</param>
    /// <param name="pageNumber">The number of the page.</param>
    /// <param name="request">The request containing the page summary details.</param>
    public async Task SubmitPageSummary(Guid paperId, int pageNumber, SubmitPageSummaryRequest request)
    {
        await daprClient.InvokeMethodAsync(HttpMethod.Put, "contentstore",
            $"papers/{paperId}/pages/{pageNumber}/summary", request);
    }

    /// <summary>
    /// Submits a paper description asynchronously.
    /// </summary>
    /// <param name="paperId">The ID of the paper.</param>
    /// <param name="request">The request containing the paper description details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SubmitPaperDescriptionAsync(Guid paperId, SubmitPaperDescriptionRequest request)
    {
        await daprClient.InvokeMethodAsync(HttpMethod.Put, "contentstore", 
            $"papers/{paperId}/description",
            request);
    }
}