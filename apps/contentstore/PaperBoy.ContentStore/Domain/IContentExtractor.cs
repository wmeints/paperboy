namespace PaperBoy.ContentStore.Domain;

/// <summary>
/// Defines a contract for extracting pages from a given URL.
/// </summary>
public interface IContentExtractor
{
    /// <summary>
    /// Asynchronously extracts pages from the specified URL.
    /// </summary>
    /// <param name="url">The URL from which to extract pages.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of extracted pages.</returns>
    Task<List<Page>> ExtractPagesAsync(string url);
}