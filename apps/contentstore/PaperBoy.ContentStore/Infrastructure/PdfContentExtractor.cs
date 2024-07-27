using PaperBoy.ContentStore.Domain;
using UglyToad.PdfPig;

namespace PaperBoy.ContentStore.Infrastructure;

/// <summary>
/// Implementation of the <see cref="IContentExtractor"/> interface.
/// </summary>
/// <param name="httpClientFactory">The HTTP client factory to use for creating a client to download the paper.</param>
public class PdfContentExtractor(IHttpClientFactory httpClientFactory): IContentExtractor
{
    private static string[] TrustedDomainNames = ["arxiv.org"];
    
    public async Task<List<Page>> ExtractPagesAsync(string url)
    {
        VerifyUrl(url);
        
        var pages = await DownloadPaperAsync(url);

        return pages;
    }

    private void VerifyUrl(string url)
    {
        var parsedUrl = new Uri(url);

        if (!TrustedDomainNames.Contains(parsedUrl.Host))
        {
            throw new ArgumentException("The provided URL doesn't come from a trusted domain");
        }
    }

    private async Task<List<Page>> DownloadPaperAsync(string url)
    {
        var results = new List<Page>();
        
        using var httpClient = httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync(url);
        
        response.EnsureSuccessStatusCode();

        using var document = PdfDocument.Open(await response.Content.ReadAsStreamAsync());

        var sourcePages = document.GetPages().ToList();

        for (int index = 0; index < sourcePages.Count(); index++)
        {
            var sourcePage = sourcePages.ElementAt(index);
            var page = new Page(index + 1, sourcePage.Text);
            
            results.Add(page);
        }

        return results;
    }
}