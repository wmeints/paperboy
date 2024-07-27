namespace PaperBoy.ContentStore.Requests;

/// <summary>
/// Represents a request to submit a score for a paper.
/// </summary>
/// <param name="Score">The score given to the paper.</param>
/// <param name="Explanation">The explanation for the given score.</param>
public record SubmitPaperScoreRequest(int Score, string Explanation);