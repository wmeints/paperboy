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
        await ScorePaperAsync(context, input.PaperId, input.Title, paperSummary);

        var paperApprovalStateChanged =
            await context.WaitForExternalEventAsync<PaperApprovalStateChangedWorkflowEvent>(
                nameof(PaperApprovalStateChangedWorkflowEvent));

        // When a paper is approved, we want to generate a nice description for it to use
        // in the weekly newsletter, on slack, and on LinkedIn. 
        if (paperApprovalStateChanged.State == ApprovalState.Approved)
        {
            var generatePaperDescriptionResult =
                await context.CallActivityAsync<GeneratePaperDescriptionActivityOutput>(
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

    private static async Task ScorePaperAsync(WorkflowContext context, Guid paperId, string paperTitle,
        string paperSummary)
    {
        var scorePaperResult = await context.CallActivityAsync<ScorePaperActivityOutput>(
            nameof(ScorePaperActivity),
            new ScorePaperActivityInput(paperTitle, paperSummary));

        await context.CallActivityAsync(nameof(SubmitPaperScoreActivity),
            new SubmitPaperScoreActivityInput(paperId, scorePaperResult.Score,
                scorePaperResult.Explanation));
    }

    private static async Task<string> SummarizePaperAsync(WorkflowContext context, Guid paperId)
    {
        var paperDetails = await context.CallActivityAsync<GetPaperDetailsActivityOutput>(
            nameof(GetPaperDetailsActivity),
            new GetPaperDetailsActivityInput(paperId));

        var pageSummaries = new List<PageSummary>();
        
        foreach (var page in paperDetails.Pages)
        {
            var summaryResult = await context.CallActivityAsync<SummarizePageActivityOutput>(
                nameof(SummarizePageActivity),
                new SummarizePageActivityInput(page.PageNumber, paperDetails.Title, page.Content));

            await context.CallActivityAsync(nameof(SubmitPageSummaryActivity),
                new SubmitPageSummaryActivityInput(paperId, page.PageNumber, summaryResult.Summary));
            
            pageSummaries.Add(new PageSummary(page.PageNumber, summaryResult.Summary));
        }

        var summarizePaperResult = await context.CallActivityAsync<SummarizePaperActivityOutput>(
            nameof(SummarizePaperActivity),
            new SummarizePaperActivityInput(paperDetails.Title, pageSummaries));

        await context.CallActivityAsync(nameof(SubmitPaperSummaryActivity),
            new SubmitPaperSummaryActivityInput(
                paperId, summarizePaperResult.Summary));

        return summarizePaperResult.Summary;
    }

    private static async Task<ImportPaperActivityOutput> ImportPaperAsync(WorkflowContext context,
        ProcessPaperWorkflowInput input)
    {
        var activityInput = new ImportPaperActivityInput(input.PaperId, input.Title, input.Url, input.Submitter);

        var activityOutput = await context.CallActivityAsync<ImportPaperActivityOutput>(
            nameof(ImportPaperActivity),
            activityInput);

        return activityOutput;
    }
}