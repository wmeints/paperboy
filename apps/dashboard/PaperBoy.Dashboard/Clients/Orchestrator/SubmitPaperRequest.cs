namespace PaperBoy.Dashboard.Clients.Orchestrator;

public record SubmitPaperRequest(string Title, string Url, string EmailAddress, string Name);