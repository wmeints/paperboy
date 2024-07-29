using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var languageModel = builder.AddConnectionString("languagemodel");

var sqlserver = builder.AddPostgres("postgres").WithDataVolume().WithInitBindMount("../../sql/initdb/");
var contentstoreDb = sqlserver.AddDatabase("contentstoredb");

var authentication = builder.AddKeycloak("authentication").WithDataVolume();

var contentstore = builder.AddProject<PaperBoy_ContentStore>("contentstore")
    .WithDaprSidecar()
    .WithReference(authentication)
    .WithReference(contentstoreDb);

var contentProcessor = builder.AddProject<PaperBoy_ContentProcessor>("contentprocessor")
    .WithDaprSidecar()
    .WithReference(languageModel)
    .WithReference(authentication)
    .WithReference(contentstore);

var orchestrator = builder.AddProject<PaperBoy_Orchestrator>("orchestrator")
    .WithDaprSidecar()
    .WithReference(contentProcessor)
    .WithReference(authentication)
    .WithReference(contentstore);

var dashboard = builder.AddNpmApp("dashboard", "../../apps/dashboard", "dev");

builder.Build().Run();
