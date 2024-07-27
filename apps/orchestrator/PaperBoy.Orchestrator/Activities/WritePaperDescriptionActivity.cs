using Dapr.Workflow;

namespace PaperBoy.Orchestrator.Activities;

public class WritePaperDescriptionActivity: WorkflowActivity<WritePaperDescriptionActivityInput, WritePaperDescriptionActivityOutput>
{
    public override Task<WritePaperDescriptionActivityOutput> RunAsync(WorkflowActivityContext context, WritePaperDescriptionActivityInput input)
    {
        // This is where we would write a nice description for the paper.
        return Task.FromResult(new WritePaperDescriptionActivityOutput("This is a great paper!"));
    }
}