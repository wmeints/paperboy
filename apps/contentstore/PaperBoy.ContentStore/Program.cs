using Marten;
using Microsoft.AspNetCore.Mvc;
using PaperBoy.ContentStore.Application.CommandHandlers;
using PaperBoy.ContentStore.Application.Projections;
using PaperBoy.ContentStore.Domain;
using PaperBoy.ContentStore.Domain.Commands;
using PaperBoy.ContentStore.Infrastructure;
using PaperBoy.ContentStore.Requests;
using PaperBoy.ContentStore.Responses;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddNpgsqlDataSource(builder.Configuration.GetConnectionString("contentstoredb")!);

var storeOptions = new StoreOptions
{
    AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate,
};

storeOptions.Projections.Add(new PaperInfoProjection(), Marten.Events.Projections.ProjectionLifecycle.Inline);

builder.Services.AddMarten(storeOptions).UseNpgsqlDataSource();

builder.Services.AddScoped<IPaperRepository, PaperRepository>();
builder.Services.AddScoped<IContentExtractor, PdfContentExtractor>();
builder.Services.AddScoped<ImportPaperCommandHandler>();
builder.Services.AddScoped<SubmitSummaryCommandHandler>();
builder.Services.AddScoped<SubmitScoreCommandHandler>();
builder.Services.AddScoped<SubmitPageSummaryCommandHandler>();

var app = builder.Build();

app.MapPost("/import", async (ImportPaperRequest form, ImportPaperCommandHandler commandHandler) =>
{
    var paperId = Guid.NewGuid();
    var importPaperCommand = new ImportPaperCommand(paperId, form.Title, form.Url, form.Submitter);

    await commandHandler.ExecuteAsync(importPaperCommand);

    return new ImportPaperResponse(paperId);
});

app.MapPut("/papers/{paperId}/summary", async (Guid paperId, SubmitPaperSummaryRequest request, SubmitSummaryCommandHandler commandHandler) =>
{
    var updateSummaryCommand = new SubmitSummaryCommand(paperId, request.Summary);

    await commandHandler.ExecuteAsync(updateSummaryCommand);

    return Results.Accepted();
});

app.MapPut("/papers/{paperId}/pages/{pageNumber}/summary", async (Guid paperId, int pageNumber,
    SubmitPageSummaryRequest request, SubmitPageSummaryCommandHandler commandHandler) =>
{
    var submitPageSummaryCommand = new SubmitPageSummaryCommand(paperId, pageNumber, request.Summary);

    await commandHandler.ExecuteAsync(submitPageSummaryCommand);

    return Results.Accepted();
});

app.MapPut("/papers/{paperId}/score", async (Guid paperId, SubmitPaperScoreRequest request, SubmitScoreCommandHandler commandHandler) =>
{
    var submitScoreCommand = new SubmitScoreCommand(paperId, request.Score, request.Explanation);

    await commandHandler.ExecuteAsync(submitScoreCommand);

    return Results.Accepted();
});


app.MapGet("/papers/{paperId}/status", async (Guid paperId, IPaperRepository paperRepository) =>
{
    var paper = await paperRepository.GetByIdAsync(paperId);

    if (paper == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(paper.Status.ToString());
});

app.MapGet("/papers/{paperId}", async (Guid paperId, IPaperRepository paperRepository) =>
{
    var paper = await paperRepository.GetByIdAsync(paperId);

    if (paper == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(paper);
});

app.MapGet("/papers", async (IPaperInfoRepository paperInfoRepository, [FromQuery] int page = 0) =>
{
    var papers = await paperInfoRepository.GetAllAsync(page, 20);
    return Results.Ok(papers);
});

app.MapDefaultEndpoints();

app.Run();