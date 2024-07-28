using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Clients.ContentStore;

public record ImportPaperRequest(string Title, string Url, SubmitterInformation Submitter);