using System.Reflection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace PaperBoy.ContentProcessor.Skills.Description.NewsletterDescription;

/// <summary>
/// Generates a newsletter description for a summarized paper.
/// </summary>
/// <param name="kernel">Semantic kernel to use.</param>
public class GenerateNewsletterDescriptionFunction(Kernel kernel)
{
    /// <summary>
    /// Executes the semantic function.
    /// </summary>
    /// <param name="title">The title of the paper.</param>
    /// <param name="summary">The summary for the paper.</param>
    /// <returns>Returns the newsletter description for the paper.</returns>
    public async Task<string> ExecuteAsync(string title, string summary)
    {
        var completionService = kernel.Services.GetRequiredService<IChatCompletionService>();
        var messages = new ChatHistory();

        messages.AddSystemMessage(ReadSystemPrompt());
        messages.AddUserMessage($"Title: {title}\r\nSummary: {summary}");

        var result = await completionService.GetChatMessageContentAsync(messages);

        return result.Content!;
    }
    
    private string ReadSystemPrompt()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "PaperBoy.ContentProcessor.Skills.Description.NewsletterDescription.instructions.txt";

        using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
        using StreamReader reader = new StreamReader(stream);
        
        return reader.ReadToEnd();
    }
}