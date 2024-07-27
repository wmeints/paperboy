namespace PaperBoy.Orchestrator.Clients.ContentStore;

/// <summary>
/// Represents a request to submit a summary for a specific page.
/// </summary>
/// <param name="Summary">The summary of the page.</param>
public record SubmitPageSummaryRequest(string Summary);