namespace PaperBoy.Dashboard.Clients.Orchestrator;

public interface IOrchestratorClient
{
    Task ApprovePaperAsync(Guid paperId);
    Task DeclinePaperAsync(Guid paperId);
    Task SubmitPaperAsync(string title, string url, string name, string emailAddress);
}
