using PaperBoy.ContentProcessor.Models;

namespace PaperBoy.ContentProcessor.Requests;

public record SummarizePaperRequest(string Title, List<PageData> Pages);
