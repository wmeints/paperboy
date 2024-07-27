using System.Reflection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using PaperBoy.ContentProcessor.Models;

namespace PaperBoy.ContentProcessor.Skills.Summarization.SummarizePaper;

/// <summary>
/// Represents a function to summarize a paper using a semantic kernel.
/// </summary>
/// <param name="kernel">The semantic kernel instance.</param>
public class SummarizePaperFunction(Kernel kernel)
{
    /// <summary>
    /// Asynchronously executes the summarization of a paper.
    /// </summary>
    /// <param name="paperTitle">The title of the paper.</param>
    /// <param name="pageSummaries">The list of summaries for each page of the paper.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the summary of the paper.</returns>
    public async Task<string> ExecuteAsync(string paperTitle, List<PageSummary> pageSummaries)
    {
        var chatCompletionService = kernel.Services.GetRequiredService<IChatCompletionService>();
        var messages = new ChatHistory();
        
        messages.AddSystemMessage(ReadSystemPrompt().Replace("{paper_title}", paperTitle));

        foreach (var pageSummary in pageSummaries)
        {
            messages.AddUserMessage(pageSummary.Summary);
        }

        var completion = await chatCompletionService.GetChatMessageContentAsync(messages);

        return completion.Content!;
    }
    
    /// <summary>
    /// Reads the system prompt from the embedded resource.
    /// </summary>
    /// <returns>The system prompt as a string.</returns>
    private string ReadSystemPrompt()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "PaperBoy.ContentProcessor.Skills.Summarization.SummarizePaper.instructions.txt";

        using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
        using StreamReader reader = new StreamReader(stream);
        
        return reader.ReadToEnd();
    }
}