using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Workflows;

/// <summary>
/// Specifies the input for the process paper workflow.
/// </summary>
public record ProcessPaperWorkflowInput(Guid PaperId, string Title, string Url, SubmitterInformation Submitter);
