namespace PaperBoy.ContentProcessor.Requests;

public record SummarizePageRequest(int PageNumber, string PaperTitle, string PageContent);