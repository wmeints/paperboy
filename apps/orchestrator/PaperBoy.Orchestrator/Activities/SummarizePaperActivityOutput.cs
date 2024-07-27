using PaperBoy.Orchestrator.Clients.ContentProcessor;
using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Activities;

public record SummarizePaperActivityOutput(string Summary, List<PageSummary> PageSummaries);