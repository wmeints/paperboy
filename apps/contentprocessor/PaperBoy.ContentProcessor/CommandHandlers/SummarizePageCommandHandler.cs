using PaperBoy.ContentProcessor.Requests;
using PaperBoy.ContentProcessor.Responses;
using PaperBoy.ContentProcessor.Skills.Summarization.SummarizePage;

namespace PaperBoy.ContentProcessor.CommandHandlers;

public class SummarizePageCommandHandler(SummarizePageFunction summarizePageFunction)
{
    public async Task<SummarizePageResponse> ExecuteAsync(SummarizePageRequest request)
    {
        var summary = await summarizePageFunction.ExecuteAsync(request.PaperTitle, request.PageContent);
        return new SummarizePageResponse(summary);
    }
}