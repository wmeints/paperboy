namespace PaperBoy.Orchestrator.Clients.ContentProcessor;

public interface IContentProcessorClient
{
    /// <summary>
    /// Generates a score for a paper with an associated explanation.
    /// </summary>
    /// <param name="request">Request data needed to score the paper.</param>
    /// <returns>Returns the generated score.</returns>
    Task<GeneratePaperScoreResponse> GeneratePaperScoreAsync(GeneratePaperScoreRequest request);

    /// <summary>
    /// Summarizes the individual pages of a paper and combines those summaries into a single summary for the whole document.
    /// </summary>
    /// <param name="request">Request data needed to generate the summaries.</param>
    /// <returns>Returns the summarized content.</returns>
    Task<SummarizePaperResponse> SummarizePaperAsync(SummarizePaperRequest request);
}