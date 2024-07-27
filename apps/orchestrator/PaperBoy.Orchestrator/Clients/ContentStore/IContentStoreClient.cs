namespace PaperBoy.Orchestrator.Clients.ContentStore;

public interface IContentStoreClient
{
    Task<ImportPaperResult> ImportPaperAsync(ImportPaperRequest request);
    Task SubmitPaperSummary(Guid paperId, SubmitPaperSummaryRequest request);
    Task SubmitPaperScoreAsync(Guid paperId, SubmitPaperScoreRequest request);
    Task<GetPaperResponse> GetPaperAsync(Guid paperId);
}