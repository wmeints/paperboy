using System.Net.Sockets;
using Dapr.Workflow;
using PaperBoy.Orchestrator.Activities;
using PaperBoy.Orchestrator.Events;
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
        await ImportPaperAsync(context, input);

        var paperSummary = await SummarizePaperAsync(context, input.PaperId);
        await ScorePaperAsync(context, input.PaperId);

        var paperApprovalStateChanged = 
            await context.WaitForExternalEventAsync<PaperApprovalStateChangedWorkflowEvent>(
                nameof(PaperApprovalStateChangedWorkflowEvent));
        
        // When a paper is approved, we want to generate a nice description for it to use
        // in the weekly newsletter, on slack, and on LinkedIn. 
        if (paperApprovalStateChanged.State == ApprovalState.Approved)
        {
            var generatePaperDescriptionResult = await context.CallActivityAsync<GeneratePaperDescriptionActivityOutput>(
                nameof(GeneratePaperDescriptionActivity),
                new GeneratePaperDescriptionActivityInput(input.Title, paperSummary));

            await context.CallActivityAsync(nameof(SubmitPaperDescriptionActivity),
                new SubmitPaperDescriptionActivityInput(input.PaperId, 
                    generatePaperDescriptionResult.Description));
        }
        else
        {
            await context.CallActivityAsync(nameof(DeclinePaperActivity),
                new DeclinePaperActivityInput(input.PaperId));
        }

        return new object();
    }

    private static async Task<GetPaperStatusActivityOutput> GetPaperStatusAsync(WorkflowContext context, ImportPaperActivityOutput importResult)
    {
        return await context.CallActivityAsync<GetPaperStatusActivityOutput>(
            nameof(GetPaperStatusActivity),
            new GetPaperStatusActivityInput(importResult.PaperId));
    }

    private static async Task ScorePaperAsync(WorkflowContext context, Guid paperId)
    {
        var scorePaperResult = await context.CallActivityAsync<ScorePaperActivityOutput>(
            nameof(ScorePaperActivity),
            new ScorePaperActivityInput(paperId));

        await context.CallActivityAsync(nameof(SubmitPaperScoreActivity),
            new SubmitPaperScoreActivityInput(paperId, scorePaperResult.Score,
                scorePaperResult.Explanation));
    }

    private static async Task<string> SummarizePaperAsync(WorkflowContext context, Guid paperId)
    {
        var paperDetails = await context.CallActivityAsync<GetPaperDetailsActivityOutput>(nameof(GetPaperDetailsActivity),
            new GetPaperDetailsActivityInput(paperId));

        foreach (var page in paperDetails.Pages)
        {
            var summaryResult = await context.CallActivityAsync<SummarizePageActivityOutput>(
                nameof(SummarizePageActivity),
                new SummarizePageActivityInput(page.PageNumber, paperDetails.Title, page.Content));

            await context.CallActivityAsync(nameof(SubmitPageSummaryActivity),
                new SubmitPageSummaryActivityInput(paperId, page.PageNumber, summaryResult.Summary));
        }
        
        var summarizePaperResult = await context.CallActivityAsync<SummarizePaperActivityOutput>(
            nameof(SummarizePaperActivity),
            new SummarizePaperActivityInput(paperId));

        await context.CallActivityAsync(nameof(SubmitPaperSummaryActivity),
            new SubmitPaperSummaryActivityInput(
                paperId,
                summarizePaperResult.Summary, 
                summarizePaperResult.PageSummaries));

        return summarizePaperResult.Summary;
    }

    private static async Task<ImportPaperActivityOutput> ImportPaperAsync(WorkflowContext context, ProcessPaperWorkflowInput input)
    {
        var activityInput = new ImportPaperActivityInput(input.PaperId, input.Title, input.Url, input.Submitter);
        
        var activityOutput = await context.CallActivityAsync<ImportPaperActivityOutput>(
            nameof(ImportPaperActivity),
            activityInput);
            
        return activityOutput;
    }
}