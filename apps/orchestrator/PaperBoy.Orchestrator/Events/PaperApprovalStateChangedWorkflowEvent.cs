using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Events;

public record PaperApprovalStateChangedWorkflowEvent(ApprovalState State);
