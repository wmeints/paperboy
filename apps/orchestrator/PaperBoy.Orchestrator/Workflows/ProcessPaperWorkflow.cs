using System.Net.Sockets;
using Dapr.Workflow;
using PaperBoy.Orchestrator.Activities;
using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Workflows;

/// <summary>
/// Implements the workflow to process papers that are submitted to the website.
/// </summary>
public class ProcessPaperWorkflow : Workflow<ProcessPaperWorkflowInput, object>
{
    /// <summary>
    /// Runs the workflow
    /// </summary>
    /// <param name="context">Workflow context to use for executing the workflow.</param>
    /// <param name="input">Input for the workflow run.</param>
    /// <returns>Returns an empty object upon completion.</returns>
    public override async Task<object> RunAsync(WorkflowContext context, ProcessPaperWorkflowInput input)
    {
        var importResult = await ImportPaperAsync(context, input);

        await SummarizePaperAsync(context, importResult);
        await ScorePaperAsync(context, importResult);

        var paperStatusResult = await GetPaperStatusAsync(context, importResult);
        
        // Wait for a human to approve or decline the paper.
        while(paperStatusResult.Status != PaperStatus.Approved && paperStatusResult.Status != PaperStatus.Declined)
        {
            await context.CreateTimer(TimeSpan.FromSeconds(5));
            paperStatusResult = await GetPaperStatusAsync(context, importResult);
        }

        // When a paper is approved, we want to generate a nice description for it to use
        // in the weekly newsletter, on slack, and on LinkedIn. 
        if (paperStatusResult.Status == PaperStatus.Approved)
        {
            var writePaperDescriptionResult = await context.CallActivityAsync<WritePaperDescriptionActivityOutput>(
                nameof(WritePaperDescriptionActivity),
                new WritePaperDescriptionActivityInput(importResult.PaperId));
        }

        return new object();
    }

    private static async Task<GetPaperStatusActivityOutput> GetPaperStatusAsync(WorkflowContext context, ImportPaperActivityOutput importResult)
    {
        return await context.CallActivityAsync<GetPaperStatusActivityOutput>(
            nameof(GetPaperStatusActivity),
            new GetPaperStatusActivityInput(importResult.PaperId));
    }

    private static async Task ScorePaperAsync(WorkflowContext context, ImportPaperActivityOutput importResult)
    {
        var scorePaperResult = await context.CallActivityAsync<ScorePaperActivityOutput>(
            nameof(ScorePaperActivity),
            new ScorePaperActivityInput(importResult.PaperId));

        await context.CallActivityAsync(nameof(SubmitPaperScoreActivity),
            new SubmitPaperScoreActivityInput(importResult.PaperId, scorePaperResult.Score,
                scorePaperResult.Explanation));
    }

    private static async Task SummarizePaperAsync(WorkflowContext context, ImportPaperActivityOutput importResult)
    {
        var paperDetails = await context.CallActivityAsync<GetPaperDetailsActivityOutput>(nameof(GetPaperDetailsActivity),
            new GetPaperDetailsActivityInput(importResult.PaperId));

        foreach (var page in paperDetails.Pages)
        {
            var summaryResult = await context.CallActivityAsync<SummarizePageActivityOutput>(
                nameof(SummarizePageActivity),
                new SummarizePageActivityInput(page.PageNumber, paperDetails.Title, page.Content));

            await context.CallActivityAsync(nameof(SubmitPageSummaryActivity),
                new SubmitPageSummaryActivityInput(importResult.PaperId, page.PageNumber, summaryResult.Summary));
        }
        
        var summarizePaperResult = await context.CallActivityAsync<SummarizePaperActivityOutput>(
            nameof(SummarizePaperActivity),
            new SummarizePaperActivityInput(importResult.PaperId));

        await context.CallActivityAsync(nameof(SubmitPaperSummaryActivity),
            new SubmitPaperSummaryActivityInput(
                importResult.PaperId,
                summarizePaperResult.Summary, 
                summarizePaperResult.PageSummaries));
    }

    private static async Task<ImportPaperActivityOutput> ImportPaperAsync(WorkflowContext context, ProcessPaperWorkflowInput input)
    {
        var importResult = await context.CallActivityAsync<ImportPaperActivityOutput>(
            nameof(ImportPaperActivity),
            new ImportPaperActivityInput(input.Url, input.Submitter));
        return importResult;
    }
}