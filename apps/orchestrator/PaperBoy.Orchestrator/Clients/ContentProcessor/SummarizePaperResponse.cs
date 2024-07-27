using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Clients.ContentProcessor;

public record SummarizePaperResponse(string Summary, List<PageSummary> PageSummaries);