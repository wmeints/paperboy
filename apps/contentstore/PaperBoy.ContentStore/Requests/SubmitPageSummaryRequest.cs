namespace PaperBoy.ContentStore.Requests;

/// <summary>
/// Represents a request to submit a page summary.
/// </summary>
/// <param name="Summary">The summary of the page.</param>
public record SubmitPageSummaryRequest(string Summary);