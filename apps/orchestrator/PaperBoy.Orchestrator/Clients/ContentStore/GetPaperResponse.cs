using PaperBoy.Orchestrator.Clients.ContentProcessor;
using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Clients.ContentStore;

public record GetPaperResponse(Guid Id,string Title, string? Summary, List<PageData> Pages);