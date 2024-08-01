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
}