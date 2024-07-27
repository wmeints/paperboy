using Microsoft.SemanticKernel;
using PaperBoy.ContentProcessor;
using PaperBoy.ContentProcessor.CommandHandlers;
using PaperBoy.ContentProcessor.Requests;
using PaperBoy.ContentProcessor.Skills.Scoring.ScorePaper;
using PaperBoy.ContentProcessor.Skills.Summarization.SummarizePage;
using PaperBoy.ContentProcessor.Skills.Summarization.SummarizePaper;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddSemanticKernel();

builder.Services.AddTransient<SummarizePaperCommandHandler>();
builder.Services.AddTransient<GeneratePaperScoreCommandHandler>();
builder.Services.AddTransient<SummarizePageCommandHandler>();
builder.Services.AddTransient<SummarizePageFunction>();
builder.Services.AddTransient<SummarizePaperFunction>();
builder.Services.AddTransient<GeneratePaperScoreFunction>();

var app = builder.Build();

app.MapPost("/Summarize", async (SummarizePaperRequest request, SummarizePaperCommandHandler commandHandler) =>
{
    var response = await commandHandler.ExecuteAsync(request);
    return Results.Ok(response);
});

app.MapPost("/SummarizePage", async (SummarizePageRequest request, SummarizePageCommandHandler commandHandler) =>
{
    var response = await commandHandler.ExecuteAsync(request);
    return Results.Ok(response);
});

app.MapPost("/GenerateScore", async (GeneratePaperScoreRequest request, GeneratePaperScoreCommandHandler commandHandler) =>
{
    var response = await commandHandler.ExecuteAsync(request);
    return Results.Ok(response);
});

app.MapDefaultEndpoints();

app.Run();
