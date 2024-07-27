using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Clients.ContentProcessor;

public record SummarizePaperRequest(string Title, List<PageData> Pages);
