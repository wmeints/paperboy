using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Clients.ContentStore;

public record SubmitPaperSummaryRequest(string Summary, List<PageSummary> PageSummaries);
