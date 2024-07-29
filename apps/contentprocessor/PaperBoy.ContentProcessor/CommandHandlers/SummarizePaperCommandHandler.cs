using PaperBoy.ContentProcessor.Models;
using PaperBoy.ContentProcessor.Requests;
using PaperBoy.ContentProcessor.Responses;
using PaperBoy.ContentProcessor.Skills.Summarization.SummarizePage;
using PaperBoy.ContentProcessor.Skills.Summarization.SummarizePaper;

namespace PaperBoy.ContentProcessor.CommandHandlers;

/// <summary>
/// Handles the command to summarize a paper.
/// </summary>
/// <param name="summarizePaperFunction">The function to summarize a paper using a semantic kernel.</param>
public class SummarizePaperCommandHandler(SummarizePaperFunction summarizePaperFunction)
{
    /// <summary>
    /// Asynchronously executes the command to summarize a paper.
    /// </summary>
    /// <param name="request">The request containing the title and pages of the paper.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with the paper summary and page summaries.</returns>
    public async Task<SummarizePaperResponse> ExecuteAsync(SummarizePaperRequest request)
    {
        var paperSummary = await summarizePaperFunction.ExecuteAsync(request.Title, request.PageSummaries);

        return new SummarizePaperResponse(paperSummary);
    }
}