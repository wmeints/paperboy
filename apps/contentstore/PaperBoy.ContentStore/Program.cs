using Marten;
using Microsoft.AspNetCore.Mvc;
using PaperBoy.ContentStore.Application.CommandHandlers;
using PaperBoy.ContentStore.Application.Projections;
using PaperBoy.ContentStore.Domain;
using PaperBoy.ContentStore.Domain.Commands;
using PaperBoy.ContentStore.Infrastructure;
using PaperBoy.ContentStore.Requests;
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
builder.Services.AddScoped<IPaperInfoRepository, PaperInfoRepository>();
builder.Services.AddScoped<IContentExtractor, PdfContentExtractor>();
builder.Services.AddScoped<ImportPaperCommandHandler>();
builder.Services.AddScoped<SubmitSummaryCommandHandler>();
builder.Services.AddScoped<SubmitScoreCommandHandler>();
builder.Services.AddScoped<SubmitPageSummaryCommandHandler>();
builder.Services.AddScoped<SubmitDescriptionCommandHandler>();
builder.Services.AddScoped<DeclinePaperCommandHandler>();
builder.Services.AddScoped<ApprovePaperCommandHandler>();

var app = builder.Build();

app.MapPost("/import", async (ImportPaperRequest form, ImportPaperCommandHandler commandHandler) =>
{
    var importPaperCommand = new ImportPaperCommand(form.PaperId, form.Title, form.Url, form.Submitter);

    await commandHandler.ExecuteAsync(importPaperCommand);

    return Results.Accepted();
});

app.MapPut("/papers/{paperId}/summary",
    async (Guid paperId, SubmitPaperSummaryRequest request, SubmitSummaryCommandHandler commandHandler) =>
    {
        try
        {
            var updateSummaryCommand = new SubmitSummaryCommand(paperId, request.Summary);

            await commandHandler.ExecuteAsync(updateSummaryCommand);

            return Results.Accepted();
        }
        catch (PaperNotFoundException)
        {
            return Results.NotFound();
        }
    });

app.MapPut("/papers/{paperId}/pages/{pageNumber}/summary", async (Guid paperId, int pageNumber,
    SubmitPageSummaryRequest request, SubmitPageSummaryCommandHandler commandHandler) =>
{
    try
    {
        var submitPageSummaryCommand = new SubmitPageSummaryCommand(paperId, pageNumber, request.Summary);

        await commandHandler.ExecuteAsync(submitPageSummaryCommand);

        return Results.Accepted();
    }
    catch (PaperNotFoundException)
    {
        return Results.NotFound();
    }
});

app.MapPut("/papers/{paperId}/score",
    async (Guid paperId, SubmitPaperScoreRequest request, SubmitScoreCommandHandler commandHandler) =>
    {
        try
        {
            var submitScoreCommand = new SubmitScoreCommand(paperId, request.Score, request.Explanation);

            await commandHandler.ExecuteAsync(submitScoreCommand);

            return Results.Accepted();
        }
        catch (PaperNotFoundException)
        {
            return Results.NotFound();
        }
    });

app.MapPut("/papers/{paperId}/description", async (Guid paperId, SubmitPaperDescriptionRequest request,
    SubmitDescriptionCommandHandler commandHandler) =>
{
    try
    {
        var submitDescriptionCommand = new SubmitDescriptionCommand(paperId, request.Description);

        await commandHandler.ExecuteAsync(submitDescriptionCommand);

        return Results.Accepted();
    }
    catch (PaperNotFoundException)
    {
        return Results.NotFound();
    }
});

app.MapPost("/papers/{paperId}/decline", async (Guid paperId, DeclinePaperCommandHandler commandHandler) =>
{
    try
    {
        await commandHandler.ExecuteAsync(new DeclinePaperCommand(paperId));
        return Results.Accepted();
    }
    catch (PaperNotFoundException)
    {
        return Results.NotFound();
    }
});

app.MapPost("/papers/{paperId}/approve", async (Guid paperId, ApprovePaperCommandHandler commandHandler) =>
{
    try
    {
        await commandHandler.ExecuteAsync(new ApprovePaperCommand(paperId));
        return Results.Accepted();
    }
    catch (PaperNotFoundException)
    {
        return Results.NotFound();
    }
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

app.MapGet("/papers/all",
    async (IPaperInfoRepository paperInfoRepository, [FromQuery] int page = 0) =>
    {
        var papers = await paperInfoRepository.GetAllAsync(page, 20);

        return Results.Ok(papers);
    });

app.MapGet("/papers/pending",
    async (IPaperInfoRepository paperInfoRepository, [FromQuery] int page = 0) =>
    {
        var papers = await paperInfoRepository.GetPendingAsync(page, 20);

        return Results.Ok(papers);
    });


app.MapDefaultEndpoints();

app.Run();