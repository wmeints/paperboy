using PaperBoy.ContentProcessor.Requests;
using PaperBoy.ContentProcessor.Responses;
using PaperBoy.ContentProcessor.Skills.Description.NewsletterDescription;

namespace PaperBoy.ContentProcessor.CommandHandlers;

public class GenerateDescriptionCommandHandler(GenerateNewsletterDescriptionFunction generateNewsletterDescriptionFunction)
{
    public async Task<GeneratePaperDescriptionResponse> ExecuteAsync(GeneratePaperDescriptionRequest request)
    {
        var description = await generateNewsletterDescriptionFunction.ExecuteAsync(request.Title, request.Summary);
        return new GeneratePaperDescriptionResponse(description);
    }
}
