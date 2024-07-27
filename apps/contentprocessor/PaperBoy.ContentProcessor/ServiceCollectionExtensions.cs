using Microsoft.SemanticKernel;

namespace PaperBoy.ContentProcessor;

public static class ServiceCollectionExtensions
{
    public static void AddSemanticKernel(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("languagemodel");
        var languageModelConnectionString = new LanguageModelConnectionString(connectionString!);

        builder.Services.AddKernel()
            .AddAzureOpenAIChatCompletion(
                deploymentName: "gpt-40",
                endpoint: languageModelConnectionString["Endpoint"],
                apiKey: languageModelConnectionString["Key"]
            );
    }
}