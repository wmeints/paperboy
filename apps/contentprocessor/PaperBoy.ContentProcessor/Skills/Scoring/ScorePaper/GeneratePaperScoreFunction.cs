using System.Reflection;
using System.Text.Json;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace PaperBoy.ContentProcessor.Skills.Scoring.ScorePaper;

/// <summary>
/// Represents a function to score a paper using a semantic kernel.
/// </summary>
/// <param name="kernel">The semantic kernel instance.</param>
public class GeneratePaperScoreFunction(Kernel kernel)
{
    /// <summary>
    /// Asynchronously executes the scoring of a paper.
    /// </summary>
    /// <param name="paperTitle">The title of the paper.</param>
    /// <param name="paperSummary">The summary of the paper.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the score of the paper.</returns>
    public async Task<PaperScore> ExecuteAsync(string paperTitle, string paperSummary)
    {
        var completionService = kernel.Services.GetRequiredService<IChatCompletionService>();
        var messages = new ChatHistory();
        
        messages.AddSystemMessage(ReadSystemPrompt());
        messages.AddUserMessage(paperSummary);

        var executionOptions = new OpenAIPromptExecutionSettings()
        {
            ResponseFormat = "json_object"
        };
        
        var completion = await completionService.GetChatMessageContentAsync(messages, executionOptions);

        return JsonSerializer.Deserialize<PaperScore>(completion.Content!)!;
    }

    private string ReadSystemPrompt()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "PaperBoy.ContentProcessor.Skills.Scoring.ScorePaper.instructions.txt";

        using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
        using StreamReader reader = new StreamReader(stream);
        
        return reader.ReadToEnd();
    }
}