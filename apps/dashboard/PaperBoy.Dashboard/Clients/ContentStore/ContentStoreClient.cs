using Dapr.Client;

namespace PaperBoy.Dashboard.Clients.ContentStore;

public interface IContentStoreClient
{
    Task<PagedResult<PaperInfo>> GetPendingPapersAsync(PaperStatus[] statuses, int pageIndex);
}

public class ContentStoreClient(DaprClient daprClient) : IContentStoreClient
{
    public async Task<PagedResult<PaperInfo>> GetPendingPapersAsync(PaperStatus[] statuses, int pageIndex)
    {
        var statusFilter = string.Join(",", statuses.Select(x => x.ToString()));

        return await daprClient.InvokeMethodAsync<PagedResult<PaperInfo>>(HttpMethod.Get, "contentstore",
            "/papers/pending");
    }
}