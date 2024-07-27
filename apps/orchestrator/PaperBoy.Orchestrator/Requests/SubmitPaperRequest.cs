namespace PaperBoy.Orchestrator.Requests;

public record SubmitPaperRequest(string Title, string Url, string EmailAddress, string Name);