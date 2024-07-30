using PaperBoy.ContentProcessor;
using PaperBoy.ContentProcessor.Requests;
using PaperBoy.ContentProcessor.Skills.Description.NewsletterDescription;
using PaperBoy.ContentProcessor.Skills.Scoring.ScorePaper;
using PaperBoy.ContentProcessor.Skills.Summarization.SummarizePage;
using PaperBoy.ContentProcessor.Skills.Summarization.SummarizePaper;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddSemanticKernel();

builder.Services.AddTransient<SummarizePageFunction>();
builder.Services.AddTransient<SummarizePaperFunction>();
builder.Services.AddTransient<GeneratePaperScoreFunction>();
builder.Services.AddTransient<GenerateNewsletterDescriptionFunction>();

var app = builder.Build();

app.MapPost("/Summarize", async (SummarizePaperRequest request, SummarizePaperFunction summarizePaperFunction) =>
{
    var summary = await summarizePaperFunction.ExecuteAsync(request.Title, request.PageSummaries);
    return Results.Ok(new { Summary = summary });
});

app.MapPost("/SummarizePage", async (SummarizePageRequest request, SummarizePageFunction summarizePageFunction) =>
{
    var summary = await summarizePageFunction.ExecuteAsync(request.PaperTitle, request.PageContent);
    return Results.Ok(new { Summary = summary });
});

app.MapPost("/GenerateScore", async (GeneratePaperScoreRequest request, GeneratePaperScoreFunction generatePaperScoreFunction) =>
{
    var score = await generatePaperScoreFunction.ExecuteAsync(request.Title, request.Summary);
    return Results.Ok(new { Score = score.Score, Explanation = score.Explanation });
});

app.MapPost("/GenerateDescription", async (GeneratePaperDescriptionRequest request, GenerateNewsletterDescriptionFunction generateNewsletterDescriptionFunction) =>
{
    var description = await generateNewsletterDescriptionFunction.ExecuteAsync(request.Title, request.Summary);
    return Results.Ok(new { Description = description });
});

app.MapDefaultEndpoints();

app.Run();
