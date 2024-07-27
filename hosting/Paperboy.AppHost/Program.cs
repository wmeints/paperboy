using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var languageModel = builder.AddConnectionString("languagemodel");

var sqlserver = builder.AddPostgres("postgres").WithDataVolume().WithInitBindMount("../../sql/");
var contentstoreDb = sqlserver.AddDatabase("contentstoredb");

var contentstore = builder.AddProject<PaperBoy_ContentStore>("contentstore")
    .WithDaprSidecar()
    .WithReference(contentstoreDb);

var contentProcessor = builder.AddProject<PaperBoy_ContentProcessor>("contentprocessor")
    .WithDaprSidecar()
    .WithReference(languageModel)
    .WithReference(contentstore);

var orchestrator = builder.AddProject<PaperBoy_Orchestrator>("orchestrator")
    .WithDaprSidecar()
    .WithReference(contentProcessor)
    .WithReference(contentstore);

builder.Build().Run();
