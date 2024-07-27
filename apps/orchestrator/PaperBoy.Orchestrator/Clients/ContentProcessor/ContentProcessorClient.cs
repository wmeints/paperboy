using Dapr.Client;

namespace PaperBoy.Orchestrator.Clients.ContentProcessor;

/// <summary>
/// Client proxy for the content processor service.
/// </summary>
/// <param name="daprClient"></param>
public class ContentProcessorClient(DaprClient daprClient): IContentProcessorClient
{
    /// <summary>
    /// Generates a score for a paper with an associated explanation.
    /// </summary>
    /// <param name="request">Request data needed to score the paper.</param>
    /// <returns>Returns the generated score.</returns>
    public async Task<GeneratePaperScoreResponse> GeneratePaperScoreAsync(GeneratePaperScoreRequest request)
    {
        return await daprClient.InvokeMethodAsync<GeneratePaperScoreRequest, GeneratePaperScoreResponse>(
            HttpMethod.Post, "contentprocessor", "GenerateScore", request);
    }
    
    /// <summary>
    /// Summarizes the individual pages of a paper and combines those summaries into a single summary for the whole document.
    /// </summary>
    /// <param name="request">Request data needed to generate the summaries.</param>
    /// <returns>Returns the summarized content.</returns>
    public async Task<SummarizePaperResponse> SummarizePaperAsync(SummarizePaperRequest request)
    {
        return await daprClient.InvokeMethodAsync<SummarizePaperRequest, SummarizePaperResponse>(
            HttpMethod.Post, "contentprocessor", "Summarize", request);
    }

    /// <summary>
    /// Summarizes an individual page of a paper.
    /// </summary>
    /// <param name="request">Request data needed to generate the summaries.</param>
    /// <returns>Returns the summarized content.</returns>
    public async Task<SummarizePageResponse> SummarizePageAsync(SummarizePageRequest request)
    {
        return await daprClient.InvokeMethodAsync<SummarizePageRequest, SummarizePageResponse>(
            HttpMethod.Post, "contentprocessor", "SummarizePage", request);
    }
}
