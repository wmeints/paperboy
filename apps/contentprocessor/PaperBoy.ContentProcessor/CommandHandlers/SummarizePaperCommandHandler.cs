﻿using PaperBoy.ContentProcessor.Models;
using PaperBoy.ContentProcessor.Requests;
using PaperBoy.ContentProcessor.Responses;
using PaperBoy.ContentProcessor.Skills.Summarization.SummarizePage;
using PaperBoy.ContentProcessor.Skills.Summarization.SummarizePaper;

namespace PaperBoy.ContentProcessor.CommandHandlers;

/// <summary>
/// Handles the command to summarize a paper.
/// </summary>
/// <param name="summarizePageFunction">The function to summarize a page using a semantic kernel.</param>
/// <param name="summarizePaperFunction">The function to summarize a paper using a semantic kernel.</param>
public class SummarizePaperCommandHandler(SummarizePageFunction summarizePageFunction, SummarizePaperFunction summarizePaperFunction)
{
    /// <summary>
    /// Asynchronously executes the command to summarize a paper.
    /// </summary>
    /// <param name="request">The request containing the title and pages of the paper.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with the paper summary and page summaries.</returns>
    public async Task<SummarizePaperResponse> ExecuteAsync(SummarizePaperRequest request)
    {
        var pageSummaries = new List<PageSummary>();

        foreach (var page in request.Pages)
        {
            var pageSummary = await summarizePageFunction.ExecuteAsync(request.Title, page.Content);
            pageSummaries.Add(new PageSummary(page.PageNumber, pageSummary));
        }
        
        var paperSummary = await summarizePaperFunction.ExecuteAsync(request.Title, pageSummaries);

        return new SummarizePaperResponse(paperSummary, pageSummaries);
    }
}