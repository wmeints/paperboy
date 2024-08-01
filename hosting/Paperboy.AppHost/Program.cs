using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var authenticationDomain = builder.AddParameter("authenticationDomain");
var authenticationClientId = builder.AddParameter("authenticationClientId");
var authenticationClientSecret = builder.AddParameter("authenticationClientSecret");

var languageModel = builder.AddConnectionString("languagemodel");

var sqlserver = builder.AddPostgres("postgres")
    .WithDataVolume().WithInitBindMount("../../sql/initdb/");

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

var dashboard = builder.AddProject<PaperBoy_Dashboard>("dashboard")
    .WithDaprSidecar()
    .WithReference(orchestrator)
    .WithReference(contentstore)
    .WithEnvironment("Auth0__Domain", authenticationDomain)
    .WithEnvironment("Auth0__ClientId", authenticationClientId)
    .WithEnvironment("Auth0__ClientSecret", authenticationClientSecret);

builder.Build().Run();
