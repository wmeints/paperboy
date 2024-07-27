using System.Reflection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace PaperBoy.ContentProcessor.Skills.Summarization.SummarizePage;

/// <summary>
/// Represents a function to summarize a page using a semantic kernel.
/// </summary>
/// <param name="kernel">The semantic kernel instance.</param>
public class SummarizePageFunction(Kernel kernel)
{
    /// <summary>
    /// Asynchronously summarizes the content of a page.
    /// </summary>
    /// <param name="paperTitle">The title of the paper.</param>
    /// <param name="content">The content of the page to be summarized.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the summary of the page.</returns>
    public async Task<string> ExecuteAsync(string paperTitle, string content)
    {
        var completionService = kernel.Services.GetRequiredService<IChatCompletionService>();
        var messages = new ChatHistory();
        
        messages.AddSystemMessage(ReadSystemPrompt().Replace("{paper_title}", paperTitle));
        messages.AddUserMessage(content);
        
        var completion = await completionService.GetChatMessageContentAsync(messages);

        return completion.Content!;
    }

    private string ReadSystemPrompt()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "PaperBoy.ContentProcessor.Skills.Summarization.SummarizePage.instructions.txt";

        using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
        using StreamReader reader = new StreamReader(stream);
        
        return reader.ReadToEnd();
    }
}