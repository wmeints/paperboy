using Dapr.Workflow;

namespace PaperBoy.Orchestrator.Activities;

public class GetPaperStatusActivity: WorkflowActivity<GetPaperStatusActivityInput, GetPaperStatusActivityOutput>
{
    public override Task<GetPaperStatusActivityOutput> RunAsync(WorkflowActivityContext context, GetPaperStatusActivityInput input)
    {
        throw new NotImplementedException();
    }
}