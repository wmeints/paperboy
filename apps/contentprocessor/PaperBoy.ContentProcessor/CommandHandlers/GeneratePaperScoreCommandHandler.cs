using PaperBoy.ContentProcessor.Requests;
using PaperBoy.ContentProcessor.Responses;
using PaperBoy.ContentProcessor.Skills.Scoring.ScorePaper;

namespace PaperBoy.ContentProcessor.CommandHandlers;

/// <summary>
/// Handles the command to generate a paper score.
/// </summary>
/// <param name="generatePaperScoreFunction">The function to score a paper using a semantic kernel.</param>
public class GeneratePaperScoreCommandHandler(GeneratePaperScoreFunction generatePaperScoreFunction)
{
    /// <summary>
    /// Asynchronously executes the command to generate a paper score.
    /// </summary>
    /// <param name="request">The request containing the title and summary of the paper.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with the score and explanation.</returns>
    public async Task<GeneratePaperScoreResponse> ExecuteAsync(GeneratePaperScoreRequest request)
    {
        var score = await generatePaperScoreFunction.ExecuteAsync(request.Title, request.Summary);
        return new GeneratePaperScoreResponse(score.Score, score.Explanation);
    }
}