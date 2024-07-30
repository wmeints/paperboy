using Dapr.Client;
using Dapr.Workflow;
using PaperBoy.Orchestrator.Activities;
using PaperBoy.Orchestrator.Clients.ContentProcessor;
using PaperBoy.Orchestrator.Clients.ContentStore;
using PaperBoy.Orchestrator.Events;
using PaperBoy.Orchestrator.Models;
using PaperBoy.Orchestrator.Requests;
using PaperBoy.Orchestrator.Workflows;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddDaprWorkflow(options =>
{
    options.RegisterActivity<ImportPaperActivity>();
    options.RegisterActivity<SubmitPaperScoreActivity>();
    options.RegisterActivity<SubmitPaperSummaryActivity>();
    options.RegisterActivity<ScorePaperActivity>();
    options.RegisterActivity<SummarizePaperActivity>();
    options.RegisterActivity<GetPaperStatusActivity>();
    options.RegisterActivity<GeneratePaperDescriptionActivity>();
    options.RegisterActivity<SummarizePageActivity>();
    options.RegisterActivity<SubmitPageSummaryActivity>();
    options.RegisterActivity<GetPaperDetailsActivity>();

    options.RegisterWorkflow<ProcessPaperWorkflow>();
});

builder.Services.AddScoped<IContentStoreClient, ContentStoreClient>();
builder.Services.AddScoped<IContentProcessorClient, ContentProcessorClient>();

var app = builder.Build();

app.MapPost("/papers", async (SubmitPaperRequest request, DaprClient daprClient) =>
{
    var paperId = Guid.NewGuid();

    var workflowInput = new ProcessPaperWorkflowInput(paperId, request.Title, request.Url,
        new SubmitterInformation(request.Name, request.EmailAddress));
    
#pragma warning disable CS0618 // Disabled because workflow is not fully supported in Dapr .NET SDK yet
    
    await daprClient.StartWorkflowAsync("dapr", nameof(ProcessPaperWorkflow), paperId.ToString(), workflowInput);
    
#pragma warning restore CS0618

    return Results.Accepted();
});

app.MapPost("/papers/{paperId}/approve", async (string paperId, DaprClient daprClient) =>
{
    var eventData = new PaperApprovalStateChangedWorkflowEvent(ApprovalState.Approved);
    var eventName = nameof(PaperApprovalStateChangedWorkflowEvent);

#pragma warning disable CS0618 // Disabled because workflow is not fully supported in Dapr .NET SDK yet
    
    await daprClient.RaiseWorkflowEventAsync(paperId, "dapr", eventName, eventData);
    
#pragma warning restore CS0618 // Type or member is obsolete

    return Results.Accepted();
});

app.MapPost("/papers/{paperId}/decline", async (string paperId, DaprClient daprClient) =>
{
    var eventData = new PaperApprovalStateChangedWorkflowEvent(ApprovalState.Declined);
    var eventName = nameof(PaperApprovalStateChangedWorkflowEvent);

#pragma warning disable CS0618 // Disabled because workflow is not fully supported in Dapr .NET SDK yet
    
    await daprClient.RaiseWorkflowEventAsync(paperId, "dapr", eventName, eventData);
    
#pragma warning restore CS0618 // Type or member is obsolete

    return Results.Accepted();
});

app.MapDefaultEndpoints();

app.Run();
