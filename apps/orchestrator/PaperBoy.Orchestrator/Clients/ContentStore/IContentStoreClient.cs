namespace PaperBoy.Orchestrator.Clients.ContentStore;

/// <summary>
/// Interface for interacting with the content store.
/// </summary>
public interface IContentStoreClient
{
    /// <summary>
    /// Imports a paper asynchronously.
    /// </summary>
    /// <param name="request">The request containing the paper details to import.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the import paper result.</returns>
    Task<ImportPaperResult> ImportPaperAsync(ImportPaperRequest request);

    /// <summary>
    /// Submits a paper summary asynchronously.
    /// </summary>
    /// <param name="paperId">The ID of the paper.</param>
    /// <param name="request">The request containing the paper summary details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SubmitPaperSummary(Guid paperId, SubmitPaperSummaryRequest request);

    /// <summary>
    /// Submits a paper score asynchronously.
    /// </summary>
    /// <param name="paperId">The ID of the paper.</param>
    /// <param name="request">The request containing the paper score details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SubmitPaperScoreAsync(Guid paperId, SubmitPaperScoreRequest request);

    /// <summary>
    /// Retrieves a paper asynchronously.
    /// </summary>
    /// <param name="paperId">The ID of the paper.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with the paper details.</returns>
    Task<GetPaperResponse> GetPaperAsync(Guid paperId);

    /// <summary>
    /// Submits a page summary asynchronously.
    /// </summary>
    /// <param name="paperId">The ID of the paper.</param>
    /// <param name="pageNumber">The number of the page.</param>
    /// <param name="request">The request containing the page summary details.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SubmitPageSummary(Guid paperId, int pageNumber, SubmitPageSummaryRequest request);
}