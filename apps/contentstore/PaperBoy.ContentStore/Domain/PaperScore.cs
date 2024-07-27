namespace PaperBoy.ContentStore.Domain;

/// <summary>
/// Represents a score for a paper with its value and an explanation.
/// </summary>
/// <param name="Value">The value of the paper score.</param>
/// <param name="Explanation">The explanation for the paper score.</param>
public record PaperScore(int Value, string Explanation);