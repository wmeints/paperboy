using Dapr.Client;

namespace PaperBoy.Dashboard.Clients.Orchestrator;

public class OrchestratorClient(DaprClient daprClient): IOrchestratorClient
{
    public async Task ApprovePaperAsync(Guid paperId)
    {
        await daprClient.InvokeMethodAsync(HttpMethod.Post, "orchestrator", $"papers/{paperId}/approve");
    }

    public async Task DeclinePaperAsync(Guid paperId)
    {
        await daprClient.InvokeMethodAsync(HttpMethod.Post, "orchestrator", $"papers/{paperId}/decline");
    }

    public async Task SubmitPaperAsync(string title, string url, string name, string emailAddress)
    {
        var request = new SubmitPaperRequest(title, url, emailAddress, name);
        await daprClient.InvokeMethodAsync(HttpMethod.Post, "orchestrator", "papers", request);
    }
}
